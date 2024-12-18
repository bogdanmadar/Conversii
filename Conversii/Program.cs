Console.WriteLine("introduceti baza initiala");
int baza1 = int.Parse(Console.ReadLine());
Console.WriteLine($"introduceti baza in care vreti sa convertiti numarul");
int baza2 = int.Parse(Console.ReadLine());
Console.WriteLine($"introduceti numarul pe care vreti sa il convertiti din baza {baza1} in baza {baza2}");
string numar = Console.ReadLine();

try
{
    string rezultat = ConvertDinBaza1inBaza2(numar, baza1, baza2);
    Console.WriteLine($"numarul {numar} din baza {baza1} in baza {baza2} este: {rezultat}");
}
catch (Exception ex)
{
    Console.WriteLine($"Eroare: {ex.Message}");
}

string ConvertDinBaza1inBaza2(string numar, int baza1, int baza2)
{
    string[] parti = numar.Split('.');
    string parteIntreaga = parti[0];
    string parteFractionara = parti.Length > 1 ? parti[1] : "";

    long parteIntreagaLaBaza10 = ConvertIntreagaLaBaza10(parteIntreaga, baza1);
    double parteFractionaraLaBaza10 = ConvertFractionaraLaBaza10(parteFractionara, baza1);

    string parteIntreagaFinal = ConvertIntregFinal(parteIntreagaLaBaza10, baza2);
    string parteFractionaraFinal = ConvertFracFinal(parteFractionaraLaBaza10, baza2, 3);

    return parteFractionaraFinal.Length > 0 ? $"{parteIntreagaFinal}.{parteFractionaraFinal}" : parteIntreagaFinal;
}

string ConvertFracFinal(double numar, int baza2, int precizie)
{
    string rezultat = "";
    int cnt = 0;
    while (numar > 0 && cnt < precizie)
    {
        numar *= baza2;
        int parteIntreaga = (int)numar;
        rezultat += CaracterPtValoare(parteIntreaga);
        numar -= parteIntreaga;
        cnt++;
    }
    return rezultat;
}

string ConvertIntregFinal(long numar, int baza2)
{
    if (numar == 0)
        return "0";
    string rezultat = "";
    while (numar > 0)
    {
        rezultat = CaracterPtValoare((int)(numar % baza2)) + rezultat;
        numar /= baza2;
    }
    return rezultat;
}

char CaracterPtValoare(int v)
{
    if (v >= 0 && v <= 9)
        return (char)('0' + v);
    if (v >= 10 && v <= 15)
        return (char)('A' + v - 10);
    throw new Exception("valoare invalida");
}

double ConvertFractionaraLaBaza10(string numar, int baza1)
{
    double rezultat = 0;
    double factor = 1.0 / baza1;
    foreach (char c in numar)
    {
        rezultat += ValCaracter(c) * factor;
        factor /= baza1;
    }
    return rezultat;
}

int ValCaracter(char c)
{
    if (char.IsDigit(c))
        return c - '0';
    if (char.IsLetter(c))
        return char.ToUpper(c) - 'A' + 10;
    throw new Exception("caracter invalid");
}

long ConvertIntreagaLaBaza10(string numar, int baza1)
{
    long rezultat = 0;
    foreach (char c in numar)
    {
        rezultat = rezultat * baza1 + ValCaracter(c);
    }
    return rezultat;
}