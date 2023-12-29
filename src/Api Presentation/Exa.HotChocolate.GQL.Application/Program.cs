using Exa.Core;
using Exa.HotChocolate.GQL.Api;

var builder = Engine.CreateAsBuilder(args);

Engine.ConfigurationFiles(builder.Configuration);

builder.Services.SetGraphQLInitialize(builder.Configuration);

Engine.Initialize(builder.Services, builder.Configuration);
Engine.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Engine.ConfigureRequestPipeline(app, builder.Environment);

app.BuildRun();