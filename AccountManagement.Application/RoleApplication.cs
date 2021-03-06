using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operationResult = new OperationResult();
            if (_roleRepository.Exists(x => x.Name == command.Name))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var role = new Role(command.Name,new List<Permission>());
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operationResult = new OperationResult();
            var role = _roleRepository.Get(command.Id);
            if (role == null)
            {
                return operationResult.Failed(QueryValidationMessage.NotFound);
            }
            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operationResult.Failed(QueryValidationMessage.DuplicateRecord);
            }

            var permissions=new List<Permission>();
            if (command.Permissions!=null)
            {
                command.Permissions.ForEach(code => permissions.Add(new Permission(code)));
            }
            role.Edit(command.Name, permissions);
            _roleRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> GetRoles()
        {
            return _roleRepository.GetRoles();
        }
    }
}
