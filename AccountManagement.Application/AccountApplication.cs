using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication:IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository accountRepository, IFileUploader fileUploader, IPasswordHasher passwordHasher, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }
        
        public OperationResult Register(CreateAccount command)
        {
            var operationResult = new OperationResult();
            if (_accountRepository.Exists(a=>a.Username==command.Username || a.Mobile==command.Mobile))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var profilePhoto = _fileUploader.FileUpload(command.ProfilePhoto, "ProfilePhotos");
            var hashedPassword = _passwordHasher.Hash(command.Password);

            var account = new Account(command.Fullname,command.Username,hashedPassword,command.Mobile,command.RoleId,profilePhoto);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operationResult = new OperationResult();
            var editAccount = _accountRepository.Get(command.Id);
            if (editAccount==null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }

            if (_accountRepository.Exists(x=>(x.Username==command.Username || x.Mobile==command.Mobile) && x.Id!=command.Id))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var profilePhoto = _fileUploader.FileUpload(command.ProfilePhoto, "ProfilePhotos");
            editAccount.Edit(command.Fullname,command.Username,command.Mobile,command.RoleId, profilePhoto);
            _accountRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account==null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }

            if (command.Password!=command.RePassword)
            {
                return operationResult.Failed(ValidationMessage.ComparePassword);
            }

            var hashedPassword = _passwordHasher.Hash(command.Password);
            account.ChangePassword(hashedPassword);
            _accountRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Login(LoginModel command)
        {
            var operationResult = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);
            if (account==null)
            {
                return operationResult.Failed(QueryValidationMessage.WrongUserPass);
            }

            (bool verified,bool needsUpgrade) result = _passwordHasher.Check(account.Password,command.Password);
            if (result.verified!=true)
            {
                return operationResult.Failed(QueryValidationMessage.WrongUserPass);
            }

            var permissions = _roleRepository.Get(account.RoleId).Permissions.Select(x => x.Code).ToList();
            var authModel=new AuthViewModel(account.Username,account.Fullname,account.Id,account.RoleId,permissions);
            _authHelper.SignIn(authModel);

            return operationResult.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }
    }
}
