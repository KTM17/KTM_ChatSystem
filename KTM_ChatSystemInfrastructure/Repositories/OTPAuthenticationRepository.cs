using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
namespace Infrastructure.Repositories;

public class OTPAuthenticationRepository
{
    private readonly KTM_CSContext _context;

    public OTPAuthenticationRepository(KTM_CSContext context)
    {
        _context = context;
    }

    public async Task CreateOTPAsync(Otpauthentication otpAuthentication)
    {
        _context.Otpauthentications.Add(otpAuthentication);
        await _context.SaveChangesAsync();
    }
}
