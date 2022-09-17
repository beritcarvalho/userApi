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
        private readonly IEmailSender _EmailSender;

        public RecoveryService(IUserRepository userRepository,
            IAccountRepository accountRepository,
            IRoleRepository roleRepository,
            IMapper mapper,
            IEmailSender emailSender)
        {
            _UserRepository = userRepository;
            _AccountRepository = accountRepository;
            _RoleRepository = roleRepository;
            _mapper = mapper;
            _EmailSender =  emailSender;
        }

        public async Task<RecoveryPasswordViewModel> ForgetPassword(RecoveryPasswordInputModel input)
        {
            try
            {
                var phone = string.Concat(input.Phone.Ddd + input.Phone.Number);
                var user = await _UserRepository.GetUserForChangePassword(input.Cpf.Number, input.Login, phone);


                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                var passwordHash = PasswordGenerator.Generate(10, true, false);

                var name = new Name();
                name.First_Name = user.Account.First_Name;
                name.Last_Name = user.Account.Last_Name;

                await _EmailSender.SendEmailNewPasswordAsync(name, passwordHash, user.Account.Email);               
           
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
                var user = await _UserRepository.GetUserForChangePassword(input.Cpf.Number, input.Login, phone);

                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                if (PasswordHasher.Verify(user.Password_Hash, input.Old_Password))
                {

                    user.Password_Hash = PasswordHasher.Hash(input.New_Password);
                    user.Last_Update_Date = DateTime.Now;

                    await _UserRepository.UpdateAsync(user);
                }

                var changePassword = _mapper.Map<ChangePasswordViewModel>(user);
                changePassword.Success = true;
                return changePassword;
            }
            catch (UserException e)
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
                return _mapper.Map<RecoveryUsernameViewModel>(user);
            }
            catch (UserException e)
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
                var user = await _UserRepository.GetUserForChangeUserName(input.Cpf.Number, input.OldLogin);

                if (user == null)
                    throw new UserException("ERR-03X01 Perfil de usuário não encontrado");

                if (PasswordHasher.Verify(user.Password_Hash, input.Password_Hash))
                {
                    user.Login = input.NewLogin;
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
            catch
            {
                throw new Exception("ERR-01X03 Falha interna no servidor");
            }

        }
    }
}
