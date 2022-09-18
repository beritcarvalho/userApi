using AutoMapper;
using SecureIdentity.Password;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Applications.Dtos.ViewModels;
using UserApi.Applications.Interfaces;
using UserApi.Domain.Exceptions;
using UserApi.Domain.Interfaces;

namespace UserApi.Applications.Services
{
    public class RecoveryService : IRecoveryService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IAccountRepository _AccountRepository;
        private readonly IRoleRepository _RoleRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _EmailService;

        public RecoveryService(IUserRepository userRepository,
            IAccountRepository accountRepository,
            IRoleRepository roleRepository,
            IMapper mapper,
            IEmailService emailSender)
        {
            _UserRepository = userRepository;
            _AccountRepository = accountRepository;
            _RoleRepository = roleRepository;
            _mapper = mapper;
            _EmailService =  emailSender;
        }

        public async Task<RecoveryPasswordViewModel> ForgetPassword(RecoveryPasswordInputModel input)
        {
            try
            {
                var phone = string.Concat(input.Phone.Ddd + input.Phone.Number);
                var user = await _UserRepository.GetUserForChangePassword(input.Cpf.Number, input.Login.Username, phone);


                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                var passwordHash = PasswordGenerator.Generate(10, true, false);

                var name = new Name
                {
                    First_Name = user.Account.First_Name,
                    Last_Name = user.Account.Last_Name
                };

                await _EmailService.SendEmailPasswordAsync(name, passwordHash, user.Account.Email, "redefinir");               
           
                user.Password_Hash = PasswordHasher.Hash(passwordHash);
                
                await _UserRepository.UpdateAsync(user);

                var changePassword = _mapper.Map<RecoveryPasswordViewModel>(user);
                return changePassword;
            }
            catch (UserException e)
            {
                throw e;
            }
            catch (EmailException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
        public async Task<ChangePasswordViewModel> ChangePassword(ChangePasswordInputModel input)
        {
            try
            {
                var phone = string.Concat(input.Phone.Ddd + input.Phone.Number);
                var user = await _UserRepository.GetUserForChangePassword(input.Cpf.Number, input.Login.Username, phone);

                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                var newPassword = input.New_Password.Password;
                var name = new Name
                {
                    First_Name = user.Account.First_Name,
                    Last_Name = user.Account.Last_Name
                };

                ChangePasswordViewModel changePassword = new ChangePasswordViewModel();
               
                if (PasswordHasher.Verify(user.Password_Hash, input.Old_Password.Password))
                { 
                    await _EmailService.SendEmailPasswordAsync(name, newPassword, user.Account.Email, "trocar");
                    
                    user.Password_Hash = PasswordHasher.Hash(newPassword);
                    user.Last_Update_Date = DateTime.Now;

                    await _UserRepository.UpdateAsync(user);
                    
                    changePassword = _mapper.Map<ChangePasswordViewModel>(user);
                    changePassword.IsSuccess(true);
                }
                
                return changePassword;
            }
            catch (UserException e)
            {
                throw e;
            }
            catch (EmailException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
        public async Task<RecoveryUsernameViewModel> GetUserName(RecoveryUserNameInputModel input)
        {
            try
            {
                var phone = string.Concat(input.Phone.Ddd + input.Phone.Number);

                var user = await _UserRepository.GetUserForLoginForget(input.Cpf.Number, phone);
                
                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                var name = new Name
                {
                    First_Name = user.Account.First_Name,
                    Last_Name = user.Account.Last_Name
                };

                await _EmailService.SendEmailUsernameAsync(name, user.Login, user.Account.Email, "resgatar");
                return _mapper.Map<RecoveryUsernameViewModel>(user);
            }
            catch (UserException e)
            {
                throw e;
            }
            catch (EmailException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }
        }
        public async Task<ChangeUserNameViewModel> ChangeUserName(ChangeUserNameInputModel input)
        {
            try
            {
                var user = await _UserRepository.GetUserForChangeUserName(input.Cpf.Number, input.OldLogin.Username);

                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                var name = new Name
                {
                    First_Name = user.Account.First_Name,
                    Last_Name = user.Account.Last_Name
                };

                if (PasswordHasher.Verify(user.Password_Hash, input.PasswordInput.Password))
                {
                    await _EmailService.SendEmailUsernameAsync(name, input.NewLogin.Username, user.Account.Email, "trocar");
                    user.Login = input.NewLogin.Username;
                    user.Last_Update_Date = DateTime.Now;

                    await _UserRepository.UpdateAsync(user);
                }

                var changeUsername = _mapper.Map<ChangeUserNameViewModel>(user);
                changeUsername.Success = true;
                return changeUsername;
            }
            catch (UserException e)
            {
                throw e;
            }
            catch (EmailException e)
            {
                throw e;
            }
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }

        }
    }
}
