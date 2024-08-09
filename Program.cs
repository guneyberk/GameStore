using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";   
//not good for concurrency
List<GameDto> games = [

    new (1,
    "Street Fighter",
    "Fighting",
    18.99M,//decimal
    new DateOnly(1992,7,15)),

    new (
    2,
    "WOW",
    "Roleplay",
    59.99M,
    new DateOnly(2010,9,30)
    ),
];
app.MapGet("games",()=>games);

app.MapGet("games/{id}",(int id)=>
{

    GameDto? game=games.Find(game => game.Id == id);
    return game is null ? Results.NotFound():Results.Ok(game);
})
    .WithName(GetGameEndpointName);//the way of doing rest 

app.MapPost("games",(CreateGameDto newGame) =>{
    GameDto game = new(
        games.Count+1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id},game);
});

app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) =>
{
    var index = games.FindIndex(game => game.Id == id);
    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price, 
        updateGame.ReleaseDate
    );
    return Results.NoContent();
});

app.MapDelete("games/{id}",(int id)=>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
    
});

app.Run();
