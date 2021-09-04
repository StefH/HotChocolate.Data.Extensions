using System;
using HotChocolate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StarWars.ExtraGraphQL;

namespace ContosoUniversity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services
            //    .AddMiniProfiler(options =>
            //    {
            //        options.RouteBasePath = "/profiler";
            //    })
            //    .AddEntityFramework();

            services
                // Needed for Blazor demo
                .AddCors()

                //.AddDbContext<SchoolContext>()
                .AddPooledDbContextFactory<SchoolContext>((sp, optionsBuilder) =>
                {
                    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                    var logger = sp.GetRequiredService<ILogger<SchoolContext>>();

                    optionsBuilder
                        .LogTo(a => logger.LogDebug(a))
                        //.UseInMemoryDatabase("uni") // "enrollments" = ok
                        //.UseSqlite("Data Source=uni.db") // "enrollments": []
                        .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=uni") // "enrollments" = ok
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging()
                        .UseLoggerFactory(loggerFactory);
                })

                .AddAutoMapper(typeof(Startup))

                .AddHttpContextAccessor()

                .AddLogging()

                // Next we are adding our GraphQL server configuration. 
                // We can host multiple named GraphQL server configurations
                // that can be exposed on different routes.
                .AddGraphQLServer()
                    .AddQueryableOffsetPagingProvider()

                    .AddQueryType<Query>()

                    // Add filtering and sorting capabilities.
                    .AddExtendedFiltering()
                    .AddSorting()

                    // Add projection
                    .AddProjections()

                    // Since we are exposing a subscription type we also need a pub/sub system 
                    // handling the subscription events. For our little demo here we will use 
                    // an in-memory pub/sub system.
                    .AddInMemorySubscriptions()

                    // Last we will add apollo tracing to our server which by default is 
                    // only activated through the X-APOLLO-TRACING:1 header.
                    .AddApolloTracing()

                    // - https://github.com/ChilliCream/hotchocolate/issues/2901
                    // - https://github.com/ChilliCream/hotchocolate/issues/3923
                    .AddDiagnosticEventListener(sp => new MyDiagnosticEventListener(sp.GetRequiredService<ILogger<MyDiagnosticEventListener>>()))
                    ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Needed for Blazor demo
            app.UseCors(policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin();
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyHeader();
            });

            app
                .UseWebSockets()
                .UseRouting()
                .UseEndpoints(endpoint => endpoint.MapGraphQL("/uni"));
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var factory = serviceScope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolContext>>();

            var context = factory.CreateDbContext();

            if (context.Database.EnsureCreated())
            {
                var course = new Course { Credits = 10, Title = "Object Oriented Programming 1" };
                context.Courses.Add(course);
                context.SaveChanges();

                var s1 = new Student { FirstMidName = "Rafael", LastName = "Foo", EnrollmentDate = DateTime.UtcNow };
                var s2 = new Student { FirstMidName = "Pascal", LastName = "Bar", EnrollmentDate = DateTime.UtcNow };
                var s3 = new Student { FirstMidName = "Michael", LastName = "Baz", EnrollmentDate = DateTime.UtcNow };
                context.Students.Add(s1);
                context.Students.Add(s2);
                context.Students.Add(s3);
                context.SaveChanges();

                var e1 = new Enrollment
                {
                    Course = course,
                    Student = s1
                };
                var e2 = new Enrollment
                {
                    Course = course,
                    Student = s2
                };
                var e3 = new Enrollment
                {
                    Course = course,
                    Student = s3
                };
                context.Enrollments.Add(e1);
                context.Enrollments.Add(e2);
                context.Enrollments.Add(e3);
                context.SaveChanges();
            }
        }
    }
}