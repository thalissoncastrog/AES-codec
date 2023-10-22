using AesCodec.Classes;
using System.Security.Cryptography;
using System.Text;

string password = null;

Console.WriteLine("AES CODEC APP (CBC MODE)\n");
Console.WriteLine("Insira o caminho para o arquivo de entrada:");

string filePath = Console.ReadLine();

if (filePath == "") {
    Console.WriteLine("Arquivo inválido");
    Console.ReadLine();
    return;
}

Console.WriteLine("Digite uma opção:");
Console.WriteLine("0 - Criptografar Arquivo.");
Console.WriteLine("1 - Descriptografar Arquivo.");

string option = Console.ReadLine();

//codec.classes.Matrix matrixPass = new codec.classes.Matrix(passwordMatrix);


string outputPath = "";

switch (option) {
    case "0":
        AesCodec.Classes.File originalFile = new AesCodec.Classes.File(@filePath);
        string textInput = originalFile.ReadFile();
        byte[] utfText = Encoding.UTF8.GetBytes(textInput);

        Console.WriteLine("Insira a senha para criptografar:");
        password = Console.ReadLine();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        Codec encode = new Codec(utfText, passwordBytes);
        Console.WriteLine("Insira o caminho para o arquivo criptografado de saída:");
        outputPath = Console.ReadLine();

        string encodedText = encode.Encode();
        originalFile.WriteFile(outputPath, encodedText);
        
        break;

    case "1":
        AesCodec.Classes.File encodedFile = new AesCodec.Classes.File(@filePath);
        string encodeText = encodedFile.ReadFile();


        byte[] utfEncodedText = Convert.FromBase64String(encodeText);

        Console.WriteLine("Insira a senha para descriptografar:");
        password = Console.ReadLine();
        byte[] encodedPassword = Encoding.UTF8.GetBytes(password);

        Codec decode = new Codec(utfEncodedText, encodedPassword);
        Console.WriteLine("Insira o caminho para o arquivo descriptografado de saída:");
        outputPath = Console.ReadLine();

        string decodedText = decode.Decode();
        encodedFile.WriteFile(outputPath, decodedText);

        break;

}