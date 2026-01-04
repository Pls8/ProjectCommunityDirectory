var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PLL_MVC_CommunityDirectory>("pll-mvc-communitydirectory");

builder.AddProject<Projects.PLL_API_CommunityDirectory>("pll-api-communitydirectory");

builder.Build().Run();
