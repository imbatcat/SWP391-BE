﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PetHealthcare.Server.Repositories.DbContext
{
    public class AuthenticationDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) :
    base(options)
        { 
        

        }
    }
}
