﻿using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication:IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;

        public AccountApplication(IAccountRepository accountRepository, IFileUploader fileUploader, IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
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