using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Authentication
{
    public class ResetPasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly EmailService _emailService;
        private readonly AuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordService(IUserRepository userRepository, EmailService emailService, 
            AuthService authService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<string, string>> ExecuteAsync(string email)
        {
            var response = new ApiResponse<string, string>();
            var user = await _userRepository.FindUserByEmailAsync(email.Trim());
            if(user == null)
            {
                response.Error = ErrorMessages.EmailDoesNotExists;
                response.Status = StatusResponseEnum.NotFound;
                return response;
            }
            var newPassword = _authService.GenerateRandomPassword();
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _emailService.SendEmailAsync(email, "Mật khẩu mới của bạn đã được đặt lại thành công", newPassword);
            await _unitOfWork.SaveChangesAsync();

            response.Body = "Reset mật khẩu thành công!";
            response.Status = StatusResponseEnum.Success;
            return response;
        }
    }
}
