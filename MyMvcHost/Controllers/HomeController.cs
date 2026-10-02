using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcHost.Dal;
using SharedModelsLib;
using SharedModelsLib.Dto;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace MyMvcHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ColvalteacherSportsdbContext context;

        public HomeController(ColvalteacherSportsdbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            List<SportDto> sports = context.Sports
                                            .Select(item => new SportDto
                                            {
                                                Id = item.Id,
                                                Name = item.Name,
                                                PlayersPerTeam = item.PlayersPerTeam,
                                                IsTeamSport = item.IsTeamSport
                                            })
                                            .ToList();
            return View(sports);
        }
        
        public IActionResult GetTeams(string sportName)
        { 
            if (sportName != null)
            {
                List<TeamDto> selectedTeams = context.Teams
                                                     .Where(t => t.Sport.Name == sportName)
                                                     .Select(t => new TeamDto
                                                     {
                                                         Id = t.Id,
                                                         Name = t.Name,
                                                         SportId = t.SportId
                                                     })
                                                     .ToList();
                return View(selectedTeams);
            }

            return RedirectToAction("index");
        }

        public IActionResult GetPlayers(string teamName)
        {
            if (teamName != null)
            {
                List<PlayerDto> selectedPlayers = context.Players
                                                            .AsNoTracking()
                                                            .Where(p => p.Team.Name == teamName)
                                                            .Select(p => new PlayerDto
                                                            {
                                                                Id = p.Id,
                                                                Name = p.Name,
                                                                Age = p.Age,
                                                                Country = p.Country,
                                                                TeamId = p.TeamId
                                                            })
                                                            .ToList(); 
                return View(selectedPlayers);
            }

            return RedirectToAction("index");
        }

        public IActionResult AddPlayerForm()
        {
            List<TeamDto> teams = context.Teams
                                                .Select(t => new TeamDto
                                                {
                                                    Id = t.Id,
                                                    Name = t.Name,
                                                    SportId = t.SportId
                                                })
                                                .ToList();
            return View(teams);
        }

        [HttpGet]
        public IActionResult AddPlayer(string joueur)
        {
            PlayerDto? playerDto = JsonSerializer.Deserialize<PlayerDto>(joueur);

            if (playerDto != null)
            {
                Player player = new Player
                {
                    Id = playerDto.Id,
                    Name = playerDto.Name,
                    Age = playerDto.Age,
                    Country = playerDto.Country,
                    TeamId = playerDto.TeamId
                };

                context.Players.Add(player);
                context.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public IActionResult FilterTeams(int minPlayers = 0)
        {
            List<PlayerDto> players = context.Players
                                                .AsNoTracking()
                                                .Select(p => new PlayerDto
                                                {
                                                    Id = p.Id,
                                                    Name = p.Name,
                                                    Age = p.Age,
                                                    Country = p.Country,
                                                    TeamId = p.TeamId
                                                })
                                                .ToList();
            Debug.WriteLine("Action exécutée");
       
            List<TeamResult> filteredTeams = players
                .GroupBy(p => p.TeamId)
                .Select(g => new TeamResult
                {
                    Name = context.Teams?.Find(g.Key).Name,
                    PlayerCount = g.Count()
                })
                .Where(t => t.PlayerCount >= minPlayers)
                .OrderByDescending(t => t.PlayerCount)
                .ToList();

            return View(filteredTeams);   
        }

        public IActionResult GetPlayersStartWith(string startToken = "")
        {
            List<PlayerDto> filteredPlayers = context.Players
                                                        .AsNoTracking()
                                                        .Where(p => p.Name.ToLower().StartsWith(startToken.ToLower()))
                                                        .Select(p => new PlayerDto
                                                        {
                                                            Id = p.Id,
                                                            Name = p.Name,
                                                            Age = p.Age,
                                                            Country = p.Country,
                                                            TeamId = p.TeamId
                                                        })
                                                        .ToList();

            return View(filteredPlayers);
        }
        
    }
}
