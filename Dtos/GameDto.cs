namespace GameStore.Api.DTOs;

//Records are immutbale perfect for DTOs because they carry data without modification
public record class GameDto(
    int Id, 
    string Name, 
    string Genre,
    decimal Price, 
    DateOnly ReleaseDate
    
    );