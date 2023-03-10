using AutoMapper;
using dotnet_rpg.dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context; 
            _mapper = mapper;
        }
        private static List<Character> characters = new List<Character>() {
            new Character(),
            new Character {
                Id = 1,
                Name = "Sam"
            }
        };
        
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);

            _context.Characters.Add(character); //Não precisa ser assíncrono pois não é uma consulta
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if(character is null)
                    throw new Exception($"Character with Id '{updateCharacter.Id}' not found");


                /*character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;*/

                //or

                _mapper.Map(updateCharacter, character);

                await _context.SaveChangesAsync(); //Só isso já realiza o update
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            
        }
        catch(Exception e) {
            serviceResponse.success = false;
            serviceResponse.message = e.Message;
        }
        return serviceResponse;
    }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                    var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

                    if(character is null)
                        throw new Exception($"Character with id '{id}' not found");
                    
                    _context.Characters.Remove(character);

                    await _context.SaveChangesAsync();

                    serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception e)
            {
                    serviceResponse.success = false;
                    serviceResponse.message = e.Message;
            }
            
            return serviceResponse;
        }
    }
}
