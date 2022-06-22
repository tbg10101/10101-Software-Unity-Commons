namespace Software10101.Units {
    public readonly struct Temperature {
        private const string Unit = "K";

        public static Temperature AbsoluteZero   = 0.0;
        public static Temperature Freezing       = 273.15;
        public static Temperature Boiling        = 373.15;
        public static Temperature MaxTemperature = double.MaxValue;

        private readonly double _kelvin;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public Temperature(double k) {
            _kelvin = k;
        }

        public static Temperature FromKelvin(double k) {
            return new Temperature(k);
        }

        public static Temperature FromCelsius(double c) {
            return new Temperature(c + 273.15);
        }

        public static Temperature FromFahrenheit(double f) {
            return new Temperature((f + 459.67) * (5.0 / 9.0));
        }

        public static implicit operator Temperature(double k) {
            return FromKelvin(k);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double ToKelvin() {
            return _kelvin;
        }

        public double ToCelsius() {
            return _kelvin - 273.15;
        }

        public double ToFahrenheit() {
            return _kelvin * (9.0 / 5.0) - 459.67;
        }

        public static implicit operator double(Temperature t) {
            return t.ToKelvin();
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Temperature operator +(Temperature first, Temperature second) {
            return first._kelvin + second._kelvin;
        }

        public static Temperature operator +(Temperature first, double second) {
            return first._kelvin + second;
        }

        public static Temperature operator +(double first, Temperature second) {
            return first + second._kelvin;
        }

        public static Temperature operator -(Temperature first, Temperature second) {
            return first._kelvin - second._kelvin;
        }

        public static Temperature operator -(Temperature first, double second) {
            return first._kelvin - second;
        }

        public static Temperature operator -(double first, Temperature second) {
            return first - second._kelvin;
        }

        public static Temperature operator *(Temperature first, double second) {
            return first._kelvin * second;
        }

        public static Temperature operator *(double first, Temperature second) {
            return first * second._kelvin;
        }

        public static Temperature operator /(Temperature first, double second) {
            return first._kelvin / second;
        }

        public static double operator /(Temperature first, Temperature second) {
            return first._kelvin / second._kelvin;
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString() {
            return ToStringKelvin();
        }

        public string ToStringKelvin() {
            return $"{ToKelvin():F2}{Unit}";
        }

        public string ToStringCelsius() {
            return $"{ToCelsius():F2}°C";
        }

        public string ToStringFahrenheit() {
            return $"{ToFahrenheit():F2}°F";
        }
    }
}
