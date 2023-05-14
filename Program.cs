using HashingWithSalt;

while (true)
{
    Console.WriteLine("Quyidagilardan birini kiriting:");
    Console.WriteLine("1 - Parolni hash qilish.");
    Console.WriteLine("2 - Parol hash'ini tekshirish.");

    string userInputOptionStr = Console.ReadLine();
    bool isUserInputOptionValid = int.TryParse(userInputOptionStr, out int userInputOption);
    if (!isUserInputOptionValid)
    {
        Console.WriteLine("Noto'g'ri son kiritildi.");
    }
    else
    {
        if (userInputOption == 1)
        {
            Console.WriteLine("Parolni kiriting:");
            string hashStr = Console.ReadLine();

            // Hash the password with salt
            string hashedPassword = HashingHelper.GetHash(hashStr);
            Console.WriteLine($"Parolning hash-qiymati: {hashedPassword}");
        }

        if (userInputOption == 2)
        {
            Console.WriteLine("Hashni kiriting:");
            string hashStr = Console.ReadLine();

            Console.WriteLine("Solishtirmoqchi bo'lgan parolni kiriting:");
            string input = Console.ReadLine();

            bool isValid = HashingHelper.IsHashValid(input, hashStr);
            string output = isValid ? "Hash to'g'ri!" : "Hash noto'g'ri.";
            Console.WriteLine(output);
            Console.WriteLine();
        }
    }
    Console.WriteLine("Dastur qaytadan ishga tushadi...");
    Console.WriteLine();
}


