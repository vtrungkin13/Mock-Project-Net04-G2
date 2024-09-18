using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public const string CampaginByIdNotFound = "Không tồn tại chiến dịch quyên góp với id này";
        public const string UserListEmpty = "Không tồn tại người dùng!";
        public const string CannotAddCampaign = "Không thể thêm chiến dịch này!";
        public const string OrganizationNameAndPhoneListsDoesnotMatch = "Số lượng số điện thoại và tên tổ chức không đồng nhất!";
        public const string CannotFindOrganizationWithThisPhoneOrName = "Không thể tìm thấy tổ chức có tên hoặc số điện thoại này!";
        public const string PhoneDoesnotMatchName = "Số điện thoại không thuộc về tổ chức này!";
        public const string CannotDeleteCampaign = "Không thể xoá chiến dịch này!";
    }
}
