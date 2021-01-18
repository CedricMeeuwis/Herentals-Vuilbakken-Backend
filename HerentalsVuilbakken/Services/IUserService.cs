using HerentalsVuilbakken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerentalsVuilbakken.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string wachtwoord);
    }
}
