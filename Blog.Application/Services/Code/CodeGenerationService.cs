using Blog.Application.Services.Configs;
using Blog.Domain.Repositories.Post;

namespace Blog.Application.Services.Code;

public interface ICodeGenerationService {
    public string GenerateCode();
}

public class CodeGenerationService(
    IPostReadRepository readRepository
) : ICodeGenerationService {
    private readonly Random _random = new();

    public string GenerateCode() {
        var charArray = new char[CodeGenerationConfiguration.NumberOfCharacters];
        var maxIndex = CodeGenerationConfiguration.ValidCharacters.Length;

        while (true) {
            for (var i = 0; i < charArray.Length; i++) {
                var charPosition = _random.Next(maxIndex);
                var character = CodeGenerationConfiguration.ValidCharacters[charPosition];

                charArray[i] = character;
            }

            var code = new string(charArray);

            if (readRepository.PostWithCodeExists(code)) {
                continue;
            }

            return code;
        }
    }
}