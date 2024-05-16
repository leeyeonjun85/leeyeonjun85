using System;
using Hotel_Booking_Backend.Models;

namespace Hotel_Booking_Backend.Authentication;

public class AuthUserService : IAuthUserService
{
    public string GetUserDetails()
    {
        return "Alex";
    }

    public bool IsValidUserInformation(LoginModel model)
    {
        if (model.UserName.Equals("Alex") && model.Password.Equals("123456")) return true;
        else return false;
    }
}

