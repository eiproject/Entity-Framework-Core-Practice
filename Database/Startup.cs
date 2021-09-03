using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFPractice.Database {
  public class Startup {
    public void ConfigureServices(IServiceCollection services)
        => services.AddDbContext<FSSContext>();
  }
}
