using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HBD.Services.Random;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Domain.MasterValues;
using PlayTogether.Web.Infrastructure.Extensions;
using PlayTogether.Web.Models.Profile;
using PlayTogether.Web.Models.Vacancy;
using Profile = PlayTogether.Domain.Profile;

namespace PlayTogether.Web.Controllers
{
    public class DummyDataController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly IPasswordHasher _passwordHasher;

        public DummyDataController(ISimpleCRUDService crudService, IPasswordHasher passwordHasher)
        {
            _crudService = crudService;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Fill()
        {
            var musicRoles = new List<MusicianRole>()
            {
                new MusicianRole() { Id = Guid.Parse("b2b09c12-0af3-e811-82a1-086266b645f5"), Title = "Барабанщик" },
                new MusicianRole() { Id = Guid.Parse("b6b09c12-0af3-e811-82a1-086266b645f5"), Title = "Трубач" },
                new MusicianRole() { Id = Guid.Parse("b4b09c12-0af3-e811-82a1-086266b645f5"), Title = "Вокалист" },
                new MusicianRole() { Id = Guid.Parse("b3b09c12-0af3-e811-82a1-086266b645f5"), Title = "Бассист" },
                new MusicianRole() { Id = Guid.Parse("bab09c12-0af3-e811-82a1-086266b645f5"), Title = "Клавишник" }
            };

            var musicGenres = new List<MusicGenre>()
            {
                new MusicGenre() { Id = Guid.Parse("bbb09c12-0af3-e811-82a1-086266b645f5"), Title = "classic" },
                new MusicGenre() { Id = Guid.Parse("bcb09c12-0af3-e811-82a1-086266b645f5"), Title = "pop" },
                new MusicGenre() { Id = Guid.Parse("bdb09c12-0af3-e811-82a1-086266b645f5"), Title = "folk" }
            };

            var cities = new List<City>()
            {
                new City() { Id = Guid.Parse("e7b09c12-0af3-e811-82a1-086266b645f5"), Title = "Киев" },
                new City() { Id = Guid.Parse("e8b09c12-0af3-e811-82a1-086266b645f5"), Title = "Александрия" },
                new City() { Id = Guid.Parse("e9b09c12-0af3-e811-82a1-086266b645f5"), Title = "Александровск" }
            };

            await CreateMusicians(musicRoles, cities);
            await CreateGroups(musicRoles, cities, musicGenres);

            return Ok();
        }

        private async Task CreateGroups(List<MusicianRole> musicRoles, List<City> cities, List<MusicGenre> musicGenres)
        {
            for (int i = 0; i < 30; i++)
            {
                var user = new User()
                {
                    PasswordHash = _passwordHasher.HashPassword("123123"),
                    Type = UserType.Group,
                    UserName = RandomGenerator.String(5) + "@gmail.com",
                    Profile = new Profile()
                };
                
                user = await _crudService.Create(user);

                await CreateMainProfile(new MainProfileModel()
                {
                    Age = RandomGenerator.Int(10, 99),
                    GroupName = RandomGenerator.String(7),
                    Description = RandomGenerator.String(256),
                    Experience = RandomGenerator.Int(0, 30),
                    IsActivated = true,
                    Name = RandomGenerator.String(12),
                    MusicGenres = new MusicGenre[]
                    {
                        musicGenres[RandomGenerator.Int(0, 3)], musicGenres[RandomGenerator.Int(0, 3)],
                    }
                }, user.Id);

                await CreateContactInfo(new ContactProfileModel()
                {
                    Address = RandomGenerator.String(14),
                    City = cities[RandomGenerator.Int(0, 3)],
                    ContactEmail = RandomGenerator.String(5) + "@gmail.com",
                    Phone1 = RandomGenerator.String(8),
                    Phone2 = RandomGenerator.String(8),
                    Url1 = RandomGenerator.String(10),
                    Url2 = RandomGenerator.String(10)
                }, user.Id);

                var vacancyTitle = RandomGenerator.String(12);
                var model = new VacancyModel()
                {
                    UserId = user.Id,
                    Description = RandomGenerator.String(256),
                    IsClosed = false,
                    Title = vacancyTitle,
                    VacancyFilter = new VacancyFilterModel()
                    {
                        MinExperience = RandomGenerator.Int(0, 30),
                        UserType = UserType.Group,
                        VacancyTitle = vacancyTitle,
                        MusicGenres = new MusicGenre[]
                        {
                            musicGenres[RandomGenerator.Int(0, 3)], musicGenres[RandomGenerator.Int(0, 3)],
                        },
                        MusicianRoles = new MusicianRole[]
                        {
                            musicRoles[RandomGenerator.Int(0, 5)], musicRoles[RandomGenerator.Int(0, 5)],
                        },
                        Cities = new City[] { cities[RandomGenerator.Int(0, 3)], cities[RandomGenerator.Int(0, 3)] }
                    }
                };
                model.Date = DateTime.Now;
                var vacancy = Mapper.Map<Vacancy>(model);
                await _crudService.Create(vacancy);
            }
        }

        private async Task CreateMusicians(List<MusicianRole> musicRoles, List<City> cities)
        {
            for (int i = 0; i < 30; i++)
            {
                var user = new User()
                {
                    PasswordHash = _passwordHasher.HashPassword("123123"),
                    Type = UserType.Musician,
                    UserName = RandomGenerator.String(5) + "@gmail.com",
                    Profile = new Profile()
                };
                user = await _crudService.Create(user);

                await CreateMainProfile(new MainProfileModel()
                {
                    Age = RandomGenerator.Int(10, 99),
                    Description = RandomGenerator.String(256),
                    Experience = RandomGenerator.Int(0, 30),
                    HasMusicianEducation = RandomGenerator.Boolean(),
                    IsActivated = true,
                    Name = RandomGenerator.String(12),
                    MusicianRoles = new MusicianRole[]
                    {
                        musicRoles[RandomGenerator.Int(0, 5)], musicRoles[RandomGenerator.Int(0, 5)],
                    }
                }, user.Id);

                await CreateContactInfo(new ContactProfileModel()
                {
                    Address = RandomGenerator.String(14),
                    City = cities[RandomGenerator.Int(0, 3)],
                    ContactEmail = RandomGenerator.String(5) + "@gmail.com",
                    Phone1 = RandomGenerator.String(8),
                    Phone2 = RandomGenerator.String(8),
                    Url1 = RandomGenerator.String(10),
                    Url2 = RandomGenerator.String(10)
                }, user.Id);
            }
        }

        private async Task CreateContactInfo(ContactProfileModel model, Guid userId)
        {
            await _crudService.Update<ContactProfileModel, User>(userId, model, (to, from) =>
            {
                if (to.Profile == null)
                {
                    to.Profile = new Profile();
                }
            });

            await _crudService.Update<ContactProfileModel, Profile>(userId, model, (to, from) =>
            {
                to.JsonCity = from.City.ToJson();
                to.ContactEmail = from.ContactEmail;
                to.Phone1 = from.Phone1;
                to.Phone2 = from.Phone2;
                to.Address = from.Address;
                to.Url1 = from.Url1;
                to.Url2 = from.Url2;
                to.JsonContactTypes = from.ContactTypes.ToJson();
                if (to.User.Type == UserType.Musician)
                {
                    var vacancy = to.User.Vacancies.FirstOrDefault();
                    vacancy.VacancyFilter.JsonCities = new[] { from.City }.ToJson();
                }
            });
        }

        private async Task CreateMainProfile(MainProfileModel model, Guid userId)
        {
            await _crudService.Update<MainProfileModel, User>(userId, model, (to, from) =>
            {
                if (to.Profile == null)
                {
                    to.Profile = new Profile();
                }
            });

            await _crudService.Update<MainProfileModel, Profile>(userId, model, (to, from) =>
            {

                to.Name = from.Name;
                to.Description = from.Description;
                to.PhotoBase64 = from.PhotoBase64;
                to.HasMusicianEducation = from.HasMusicianEducation;
                to.JsonMusicGenres = from.MusicGenres?.ToJson();

                if (to.User.Type == UserType.Musician)
                {
                    to.Age = from.Age;
                    to.Experience = from.Experience;
                    to.JsonWorkTypes = from.WorkTypes?.ToJson();
                    to.JsonMusicianRoles = from.MusicianRoles?.ToJson();
                    to.IsActivated = from.IsActivated;

                    to.User.Vacancies.Add(new Vacancy()
                    {
                        Title = from.Name,
                        Description = from.Description,
                        IsClosed = false,
                        Date = DateTime.Now,
                        VacancyFilter = new VacancyFilter()
                        {
                            JsonMusicianRoles = from.MusicianRoles?.ToJson(),
                            JsonMusicGenres = from.MusicGenres?.ToJson(),
                            JsonWorkTypes = from.WorkTypes?.ToJson()
                        }
                    });
                }
                else if (to.User.Type == UserType.Group)
                {
                    to.GroupName = from.GroupName;
                }
            });
        }
    }
}