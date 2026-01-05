namespace DesignPattern_Factory2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Building[] Buildings = new Building[2];

            Buildings[0] = new Barracks();
            Buildings[1] = new StarPort();

            List<Unit> ltAllUnit = new List<Unit>();
            ltAllUnit.Add(Buildings[0].makeUnit("적 마린"));
            ltAllUnit.Add(Buildings[1].makeUnit("아군 드랍쉽"));

            ltAllUnit[0].Move("언덕");
            Unit unitMarine = ltAllUnit[0];
            ltAllUnit[1].Attacked(ref unitMarine);

            Console.ReadLine();
        }

    }

    public abstract class Unit
    {

        public string m_strName;
        public int m_intAttackPower;
        public int m_intHealth;

        public abstract void Move(string _strPoint);
        public abstract void Attacked(ref Unit _unitTarget);

    }

    public class Marine : Unit
    {

        public Marine(string _strName)
        {
            this.m_strName = _strName;
            this.m_intAttackPower = 15;
            this.m_intHealth = 100;
            Console.WriteLine(_strName + " : 생성 완료");
        }

        public override void Move(string _strPoint)
        {
            Console.WriteLine(m_strName + " : " + _strPoint + " 이동 완료");
        }


        public override void Attacked(ref Unit _unitTarget)
        {

            this.m_intHealth -= _unitTarget.m_intAttackPower;
            Console.WriteLine(m_strName + " 공격당함 : 공격자->" + _unitTarget.m_strName + " : 남은체력 " + this.m_intHealth.ToString());
        }


    }

    public class Dropship : Unit
    {

        public Dropship(string _strName)
        {
            this.m_strName = _strName;
            this.m_intAttackPower = 0;
            this.m_intHealth = 100;
            Console.WriteLine(_strName + " : 생성 완료");
        }


        public override void Move(string _strPoint)
        {
            Console.WriteLine(m_strName + " : " + _strPoint + " 이동 완료");
        }

        public override void Attacked(ref Unit _unitTarget)
        {
            this.m_intHealth -= _unitTarget.m_intAttackPower;
            Console.WriteLine(m_strName + " 공격당함 : 공격자->" + _unitTarget.m_strName + " : 남은체력 " + this.m_intHealth.ToString());
        }
    }

    public abstract class Building
    {
        public abstract Unit makeUnit(string _strName);
    }

    public class Barracks : Building
    {

        public override Unit makeUnit(string _strName)
        {
            return new Marine(_strName);
        }
    }

    public class StarPort : Building
    {

        public override Unit makeUnit(string _strName)
        {
            return new Dropship(_strName);
        }
    }
}