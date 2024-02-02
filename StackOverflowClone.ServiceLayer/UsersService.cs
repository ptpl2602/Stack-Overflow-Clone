﻿using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModels;
using StackOverflowClone.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowClone.ServiceLayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel viewModel);
        void UpdateUserDetails(EditUserDetailsViewModel viewModel);
        void UpdateUserPassword(EditUserPasswordViewModel viewModel);
        void DeleteUser(int userId);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        UserViewModel GetUsersByEmail (string Email);
        UserViewModel GetUsersByID(int UserID);
    }
    public class UsersService : IUsersService
    {
        IUsersRepository iUsersRepo;
        public UsersService()
        {
            iUsersRepo = new UsersRepository();
        }
        public int InsertUser(RegisterViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<RegisterViewModel, User>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<RegisterViewModel, User>(viewModel);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(viewModel.Password);
            iUsersRepo.InsertUser(user);

            int userId = iUsersRepo.GetLatesUserID();
            return userId;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<EditUserDetailsViewModel, User>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserDetailsViewModel, User>(viewModel);
            iUsersRepo.UpdateUserDetails(user);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel viewModel)
        {
            var config = new MapperConfiguration(i => { i.CreateMap<EditUserPasswordViewModel, User>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserPasswordViewModel, User>(viewModel);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(viewModel.Password);
            iUsersRepo.UpdateUserPassword(user);
        }

        public void DeleteUser(int userId)
        {
            iUsersRepo.DeleteUser(userId);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> users = iUsersRepo.GetUsers();
            var config = new MapperConfiguration(i => { i.CreateMap<User, UserViewModel>(); i.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> viewModel = mapper.Map<List<User>, List<UserViewModel>>(users);
            return viewModel;
        }

        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            User user = iUsersRepo.GetUserByEmailAndPasswd(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            UserViewModel viewModel = null;
            if (user != null)
            {
                var config = new MapperConfiguration(i => { i.CreateMap<User, UserViewModel>(); i.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                viewModel = mapper.Map<User, UserViewModel>(user);
            }
            return viewModel;
        }

        public UserViewModel GetUsersByEmail(string Email)
        {
            User user = iUsersRepo.GetUserByEmail(Email).FirstOrDefault();
            UserViewModel viewModel = null;
            if (user != null)
            {
                var config = new MapperConfiguration(i => { i.CreateMap<User, UserViewModel>(); i.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                viewModel = mapper.Map<User, UserViewModel>(user);
            }
            return viewModel;
        }
        public UserViewModel GetUsersByID(int UserID)
        {
            User user = iUsersRepo.GetUserByID(UserID).FirstOrDefault();
            UserViewModel viewModel = null;
            if (user != null)
            {
                var config = new MapperConfiguration(i => { i.CreateMap<User, UserViewModel>(); i.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                viewModel = mapper.Map<User, UserViewModel>(user);
            }
            return viewModel;
        }
    }
}