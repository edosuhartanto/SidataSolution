// Program.cs — console app sekali pakai untuk generate key pair
using System.Security.Cryptography;
using System.Text;

using var rsa = RSA.Create(2048);

var privatePem = rsa.ExportRSAPrivateKeyPem();
var publicPem = rsa.ExportRSAPublicKeyPem();

File.WriteAllText("private.pem", privatePem);
File.WriteAllText("public.pem", publicPem);

Console.WriteLine("Private key:");
Console.WriteLine(privatePem);
Console.WriteLine();
Console.WriteLine("Public key:");
Console.WriteLine(publicPem);

// Encode seluruh PEM (termasuk newline-nya) jadi base64 satu baris
var privateBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(privatePem));
var publicBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(publicPem));

Console.WriteLine("PRIVATE (base64, simpan ini ke env var):");
Console.WriteLine(privateBase64);
Console.WriteLine();
Console.WriteLine("PUBLIC (base64, simpan ini ke env var):");
Console.WriteLine(publicBase64);

Console.ReadLine();