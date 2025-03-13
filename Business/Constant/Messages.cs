using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constant
{
    public static class Messages
    {
        public static string AccessTokenCreated = "Giriş Başarılı";
        public static string AuthorizationDenied = "Yetkiniz Yok";
        public static string UserRegistered = "Kullanıcı Eklendi";
        public static string UserUpdated = "Kullanıcı GÜncellendi";
        public static string UserDeleted = "Kullanıcı Silindi";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string PasswordError = "Şifre Hatalı";
        public static string PasswordSuccess = "Şifre Başarılı";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UsersListed = "Kullanıcılar Listelendi";
        public static string UserInfoListed = "Kullanıcı Bilgileri Listelendi";
        public static string UserAlreadyExists = "Kullanıcı Zaten Mevcut";
        public static string UserRoleUpdated = "Kullanıcı Rolü Güncellendi";
        public static string UserPasswordUpdated = "Kullanıcı Şifresi Güncellendi";
        public static string UserEmailUpdated = "Kullanıcı E-Posta Adresi Güncellendi";

        public static string UserOperationClaimAdded = "Rol Eklendi";
        public static string OperationClaimAdded = "Rol Adı Eklendi";
        public static string PageClaimAdded = "Rol Yetkileri Eklendi";
        public static string RoleListed = "Roller Listelendi";
        public static string RoleNotFound = "Rol Bulunamadı";
        public static string RoleAlreadyExists = "Rol Zaten Mevcut";
        public static string PageClaimsDeleted = "Rol Yetkileri Silindi";

        public static string ProjectAdded = "Project Eklendi";
        public static string ProjectAddError = "Project Eklenirken Hata İle Karşılaşıldı";
        public static string ProjectUpdated = "Project Güncellendi";
        public static string ProjectNotFound = "Project Bulunamadı";
        public static string ProjectInfoListed = "Project Bilgileri Listelendi";
        public static string ProjectDeleted = "Project Silindi";

        public static string BlogAdded = "Blog Eklendi";
        public static string BlogAddError = "Blog Eklenirken Hata İle Karşılaşıldı";
        public static string BlogUpdated = "Blog Güncellendi";
        public static string BlogNotFound = "Blog Bulunamadı";
        public static string BlogInfoListed = "Blog Bilgileri Listelendi";
        public static string BlogDeleted = "Blog Silindi";

        public static string CommentAdded = "Yorum Eklendi";
        public static string CommentUpdated = "Yorum Güncellendi";
        public static string CommentNotFound = "Yorum Bulunamadı";
        public static string CommentInfoListed = "Yorum Bilgileri Listelendi";
        public static string CommentDeleted = "Yorum Silindi";

        public static string TagAdded = "Tag Eklendi";
        public static string TagDeleted = "Tag Silindi";

        public static string BlogImageAdded = "Blog Görselleri Eklendi";
        public static string BlogImageDeleted = "Blog Görselleri Silindi";
        public static string BlogImageInfoListed = "Blog Görsel Bilgileri Listelendi";

        public static string AboutAdded = "Hakkımda Eklendi";
        public static string AboutUpdated = "Hakkımda Güncellendi";
        public static string AboutNotFound = "Hakkımda Bulunamadı";
        public static string AboutInfoListed = "Hakkımda Bilgileri Listelendi";
        public static string AboutDeleted = "Hakkımda Silindi";

        public static string ResumeAdded = "Özgeçmiş Eklendi";
        public static string ResumeUpdated = "Özgeçmiş Güncellendi";
        public static string ResumeNotFound = "Özgeçmiş Bulunamadı";
        public static string ResumeInfoListed = "Özgeçmiş Bilgileri Listelendi";
        public static string ResumeDeleted = "Özgeçmiş Silindi";

        public static string ContactAdded = "Mesaj Gönderildi";
        public static string ContactUpdated = "Mesaj Güncellendi";
        public static string ContactNotFound = "Mesaj Bulunamadı";
        public static string ContactInfoListed = "Mesaj Bilgileri Listelendi";
        public static string ContactDeleted = "Mesaj Silindi";
    }

}
