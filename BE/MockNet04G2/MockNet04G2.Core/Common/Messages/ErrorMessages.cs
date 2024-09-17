using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Common.Messages
{
    public static class ErrorMessages
    {
        public const string UserNameOrPasswordWrong = "Tên đăng nhập hoặc mật khẩu sai";
        public const string EmailAlreadyExists = "Email đã tồn tại!";
        public const string PhonelAlreadyExists = "Số điện thoại đã tồn tại!";
        public const string ConfirmPasswordDoesNotMatch = "Mật khẩu xác nhận không khớp";
        public const string ChangeRoleFailed = "Tài khoản này chưa được cập nhật quyền hạn!";
        public const string CannotGetUser = "Không thể lấy được người dùng!";
        public const string OldPasswordIsIncorrect = "Mật khẩu không đúng!";
        public const string NewPasswordCanNotBeTheSameOldPassword = "Mật khẩu mới không được trùng với mật khẩu cũ!";


    }
}
