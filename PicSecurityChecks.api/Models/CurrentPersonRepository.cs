﻿using PicSecurityChecks.api.Interface;
using PicSecurityChecks.Shared;
using System.Security.Principal;

namespace PicSecurityChecks.api.Models
{
    public class CurrentPersonRepository : ICurrentPersonRepository
    {
        public CurrentPerson GetCurrentPerson()
        {
            CurrentPerson person = new CurrentPerson();
           person.Name = WindowsIdentity.GetCurrent().Name.ToString();
            return person;
        }
    }
}
