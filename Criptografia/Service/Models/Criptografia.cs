using Criptografia.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
    public class Criptografia
    {
        private const char V = '*';
        private CryptProvider _cryptProvider;
        private SymmetricAlgorithm _algorithm;

        /// <summary>
        /// Inicialização do vetor do algoritmo simétrico
        /// </summary>
        private void SetIV()
        {
            switch (_cryptProvider)
            {
                case CryptProvider.Rijndael:
                    _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    break;

                default:
                    _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    break;
            }
        }

        /// <summary>
        /// Chave secreta para o algoritmo simétrico de criptografia.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Contrutor padrão da classe, é setado um tipo de criptografia padrão (Rijndael).
        /// </summary>
        public Criptografia()
        {
            _algorithm = new RijndaelManaged();

            _algorithm.Mode = CipherMode.CBC;

            _cryptProvider = CryptProvider.Rijndael;
        }

        /// <summary>
        /// Construtor com o tipo de criptografia a ser usada Você pode escolher o tipo pelo Enum chamado CryptProvider.
        /// </summary>
        /// <param name="cryptProvider">Tipo de criptografia.</param>
        public Criptografia(CryptProvider cryptProvider)
        {
            // Seleciona algoritmo simétrico

            switch (cryptProvider)

            {
                case CryptProvider.Rijndael:

                    _algorithm = new RijndaelManaged();

                    _cryptProvider = CryptProvider.Rijndael;

                    break;

                case CryptProvider.RC2:

                    _algorithm = new RC2CryptoServiceProvider();

                    _cryptProvider = CryptProvider.RC2;

                    break;

                case CryptProvider.DES:

                    _algorithm = new DESCryptoServiceProvider();

                    _cryptProvider = CryptProvider.DES;

                    break;

                case CryptProvider.TripleDES:

                    _algorithm = new TripleDESCryptoServiceProvider();

                    _cryptProvider = CryptProvider.TripleDES;

                    break;
            }

            _algorithm.Mode = CipherMode.CBC;
        }

        /// <summary>
        /// Gera a chave de criptografia válida dentro do array.
        /// </summary>
        /// <returns>Chave com array de bytes.</returns>
        public byte[] GetKey()
        {
            string salt = string.Empty;

            // Ajusta o tamanho da chave se necessário e retorna uma chave válida

            if (_algorithm.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits

                int keySize = Key.Length * 8;

                int minSize = _algorithm.LegalKeySizes[0].MinSize;

                int maxSize = _algorithm.LegalKeySizes[0].MaxSize;

                int skipSize = _algorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)

                {
                    // Busca o valor máximo da chave

                    Key = Key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)

                {
                    // Seta um tamanho válido

                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;

                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho

                        Key = Key.PadRight(validSize / 8, V);
                    }
                }
            }

            PasswordDeriveBytes key = new PasswordDeriveBytes(Key, ASCIIEncoding.ASCII.GetBytes(salt));

            return key.GetBytes(Key.Length);
        }

        /// <summary>
        /// Encripta o dado solicitado.
        /// </summary>
        /// <param name="plainText">Texto a ser criptografado.</param>
        /// <returns>Texto criptografado.</returns>
        public string Encrypt(string texto)
        {
            byte[] plainByte = Encoding.UTF8.GetBytes(texto);

            byte[] keyByte = GetKey();

            // Seta a chave privada

            _algorithm.Key = keyByte;

            SetIV();

            // Interface de criptografia / Cria objeto de criptografia

            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();

            MemoryStream _memoryStream = new MemoryStream();

            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

            // Grava os dados criptografados no MemoryStream

            _cryptoStream.Write(plainByte, 0, plainByte.Length);

            _cryptoStream.FlushFinalBlock();

            // Busca o tamanho dos bytes encriptados

            byte[] cryptoByte = _memoryStream.ToArray();

            // Converte para a base 64 string para uso posterior em um xml

            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        /// <summary>
        /// Desencripta o dado solicitado.
        /// </summary>
        /// <param name="cryptoText">Texto a ser descriptografado.</param>
        /// <returns>Texto descriptografado.</returns>
        public string Decrypt(string textoCriptografado)
        {
            // Converte a base 64 string em num array de bytes

            byte[] cryptoByte = Convert.FromBase64String(textoCriptografado);

            byte[] keyByte = GetKey();

            // Seta a chave privada

            _algorithm.Key = keyByte;

            SetIV();

            // Interface de criptografia / Cria objeto de descriptografia

            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();

            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);
                // Busca resultado do CryptoStream

                StreamReader _streamReader = new StreamReader(_cryptoStream);

                return _streamReader.ReadToEnd();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
