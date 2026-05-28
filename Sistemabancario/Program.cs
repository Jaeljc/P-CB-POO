using System;
using System.Collections.Generic;

// ABSTRACCIÓN: clase base abstracta
abstract class CuentaBancaria
{
    // ENCAPSULAMIENTO: atributos privados 
    private string numero;
    private string titular;
    private double saldo;

    public string Numero => numero;
    public string Titular => titular;
    public double Saldo => saldo;

    public CuentaBancaria(string numero, string titular, double saldo)
    {
        this.numero = numero;
        this.titular = titular;
        this.saldo = saldo;
    }

    public void Depositar(double monto)
    {
        saldo += monto;
        Console.WriteLine($"Deposito exitoso. Saldo actual: {saldo}");
    }

    public void Retirar(double monto)
    {
        if (monto > saldo) { Console.WriteLine("Saldo insuficiente."); return; }
        saldo -= monto;
        Console.WriteLine($"Retiro exitoso. Saldo actual: {saldo}");
    }

    // POLIMORFISMO: cada subclase lo implementa diferente
    public abstract void MostrarInfo();
    public abstract double CalcularInteres();
}

// HERENCIA: subclases
class CuentaAhorros : CuentaBancaria
{
    private double tasa;

    public CuentaAhorros(string numero, string titular, double saldo, double tasa)
        : base(numero, titular, saldo)
    {
        this.tasa = tasa;
    }

    public override double CalcularInteres() => Saldo * tasa;

    public override void MostrarInfo()
    {
        Console.WriteLine($"[Ahorros] {Numero} - {Titular}");
        Console.WriteLine($"  Saldo: {Saldo}  |  Interes anual: {CalcularInteres()}");
    }
}

class CuentaCorriente : CuentaBancaria
{
    private double limiteCredito;

    public CuentaCorriente(string numero, string titular, double saldo, double limiteCredito)
        : base(numero, titular, saldo)
    {
        this.limiteCredito = limiteCredito;
    }

    public override double CalcularInteres() => 0; // no genera interes

    public override void MostrarInfo()
    {
        Console.WriteLine($"[Corriente] {Numero} - {Titular}");
        Console.WriteLine($"  Saldo: {Saldo}  |  Limite de credito: {limiteCredito}");
    }
}

class CuentaInversion : CuentaBancaria
{
    private double rendimiento;

    public CuentaInversion(string numero, string titular, double saldo, double rendimiento)
        : base(numero, titular, saldo)
    {
        this.rendimiento = rendimiento;
    }

    public override double CalcularInteres() => Saldo * rendimiento;

    public override void MostrarInfo()
    {
        Console.WriteLine($"[Inversion] {Numero} - {Titular}");
        Console.WriteLine($"  Saldo: {Saldo}  |  Rendimiento anual: {CalcularInteres()}");
    }
}

// PROGRAMA PRINCIPAL
class Program
{
    static List<CuentaBancaria> cuentas = new List<CuentaBancaria>();

    static void Main()
    {
        // Datos de prueba
        cuentas.Add(new CuentaAhorros("960000001", "Ana Garcia", 5000, 0.05));
        cuentas.Add(new CuentaCorriente("960000002", "Luis Martinez", 2000, 3000));
        cuentas.Add(new CuentaInversion("960000003", "Maria Lopez", 10000, 0.08));

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n--- BANCO POO ---");
            Console.WriteLine("1. Ver todas las cuentas");
            Console.WriteLine("2. Depositar");
            Console.WriteLine("3. Retirar");
            Console.WriteLine("4. Ver intereses (polimorfismo)");
            Console.WriteLine("5. Salir");
            Console.Write("Opcion: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine();
                    foreach (var c in cuentas) c.MostrarInfo();
                    break;

                case "2":
                    Console.Write("Numero de cuenta: ");
                    var c2 = cuentas.Find(c => c.Numero == Console.ReadLine());
                    if (c2 == null) { Console.WriteLine("Cuenta no encontrada."); break; }
                    Console.Write("Monto: ");
                    c2.Depositar(double.Parse(Console.ReadLine()));
                    break;

                case "3":
                    Console.Write("Numero de cuenta: ");
                    var c3 = cuentas.Find(c => c.Numero == Console.ReadLine());
                    if (c3 == null) { Console.WriteLine("Cuenta no encontrada."); break; }
                    Console.Write("Monto: ");
                    c3.Retirar(double.Parse(Console.ReadLine()));
                    break;

                case "4":
                    Console.WriteLine("\nPolimorfismo - mismo metodo, diferente resultado:");
                    foreach (var c in cuentas)
                        Console.WriteLine($"  {c.GetType().Name,-20} -> Interes: {c.CalcularInteres()}");
                    break;

                case "5":
                    salir = true;
                    break;
            }
        }
    }
}