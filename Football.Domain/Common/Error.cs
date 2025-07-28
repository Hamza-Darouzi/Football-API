

namespace Football.Domain.Common;



public class Error : IEquatable<Error>
{
    public static readonly Error None = new("Error.None", "الأمور طيبة", "OK");
    public static readonly Error NullValue = new("Error.NullValue", "محتوى غير موجود", "Content Not Found");
    public static readonly Error FileExist = new("Error.FileExist", "ملف موجود مسبقاً", "File Already Exist");
    public static readonly Error InvalidFile = new("Error.NotValidFile", "ملف غير صالح", "Invalid File");
    public static readonly Error PhoneNumberExist = new("Error.PhoneNumberExist", "رقم الهاتف موجود مسبقاً", "Phone Number Already Exist");
    public static readonly Error UserNotFound = new("Error.UserNotFound", "عذراً العميل غير مسجّل , يرجى إنشاء حساب", "User Is Not Registerd , Please Create New Account Then Try Again");
    public static readonly Error WrongPassword = new("Error.WrongPassword", "كلمة مرور خاطئة ", "Incorrect Password");
    public static readonly Error PhoneNumberConfirmation = new("Error.PhoneNumberConfirmation", "رقم الهاتف غير فعّال", "Phone Number Is Not Ative");
    public static readonly Error InvalidPhoneNumber = new("Error.InvalidPhoneNumber", "رقم الهاتف غير صحيح , يرجى التأكد منه ثمّ إعادة المحاولة", "Invalid Phone Number");
    public static readonly Error InvalidValue = new("Error.InvalidValue", "خطأ في البيانات", "Wrong Data");
    public static readonly Error InvalidOTP = new("Error.InvalidOTP", "كود التفعيل خاطئ !", "Invalid OTP");
    public static readonly Error ConversionFailed = new("Error.ConversionFailed", "لا يمكن معالجة صيغة هذا الملف", "File Extension Can't Be Proccesed");

    public Error(string code, string message, string latinoMessage)
    {
        Code = code;
        Message = message;
        LatinoMessage = latinoMessage;
    }
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    public string Code { get; }

    public string? Message { get; }
    public string? LatinoMessage { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override string ToString() => Code;
}


/*
 
public class Error : IEquatable<Error>
{
    public static readonly Error None = new("Error.None", "OK");
    public static readonly Error NullValue = new("Error.NullValue", "Content Not Found");
    public static readonly Error FileExist = new("Error.FileExist", "File Already Exist");
    public static readonly Error InvalidFile = new("Error.NotValidFile", "Invalid File");
    public static readonly Error PhoneNumberExist = new("Error.PhoneNumberExist", "Phone Number Already Exist");
    public static readonly Error UserNotFound = new("Error.UserNotFound", "User Is Not Registered , Please Create New Account");
    public static readonly Error WrongPassword = new("Error.WrongPassword", "Invalid Password");
    public static readonly Error PhoneNumberConfirmation = new("Error.PhoneNumberConfirmation", "Phone Number Is Not Activated");
    public static readonly Error InvalidPhoneNumber = new("Error.InvalidPhoneNumber", "Phone Number Is Not Correct");
    public static readonly Error InvalidValue = new("Error.InvalidValue", "Wrong Data");
    public static readonly Error InvalidOTP = new("Error.InvalidOTP", "Invalid OTP");
    public static readonly Error ConversionFailed = new("Error.ConversionFailed", "File Extension Can't be Processed");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    public string Code { get; }

    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(Code, Message);

    public override string ToString() => Code;
}
*/