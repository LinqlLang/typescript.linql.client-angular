using Linql.Core.Test;
using Linql.Test.Files;

namespace Linql.Client.Test.Expressions
{
    public class Smoke_Test : TestFileTests
    {
        protected LinqlContext Context { get; set; } = new LinqlContext();

        protected override string TestFolder { get; set; } = "Smoke";

        [Test]
        //Should fail.
        public void IncorrectToJson()
        {
            List<int> listType = new List<int>();

            try
            {
                listType.AsQueryable().ToJson();
            }
            catch (System.Exception ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        [Test]
        //Should fail.
        public async Task IncorrectToJsonAsync()
        {
            List<int> listType = new List<int>();

            try
            {
                await listType.AsQueryable().ToJsonAsync();
            }
            catch (System.Exception ex)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }


        [Test]
        public void EmptySearch()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string empty = search.ToJson();

            TestLoader.Compare(nameof(Smoke_Test.EmptySearch), empty);
        }

        [Test]
        public async Task SimpleConstant()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => true).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.SimpleConstant), simpleConstant);
        }

        [Test]
        public async Task SimpleBooleanProperty()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.Boolean).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.SimpleBooleanProperty), simpleConstant);
        }

        [Test]
        public async Task BooleanNegate()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => !r.Boolean).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.BooleanNegate), simpleConstant);
        }

        [Test]
        public async Task SimpleBooleanPropertyChaining()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.OneToOne.Boolean).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.SimpleBooleanPropertyChaining), simpleConstant);
        }

        [Test]
        public async Task SimpleBooleanPropertyEquals()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.Boolean == false).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.SimpleBooleanPropertyEquals), simpleConstant);
        }

        [Test]
        public async Task SimpleBooleanPropertyEqualsSwap()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => false == r.Boolean).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.SimpleBooleanPropertyEqualsSwap), simpleConstant);
        }

        [Test]
        public async Task TwoBooleans()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => false == true).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.TwoBooleans), simpleConstant);
        }

        [Test]
        public async Task BooleanVar()
        {
            bool test = false;
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => false == test).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.BooleanVar), simpleConstant);
        }

        [Test]
        public async Task ComplexBoolean()
        {
            DataModel test = new DataModel();
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => test.Boolean).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.ComplexBoolean), simpleConstant);
        }

        [Test]
        public async Task ComplexBooleanAsArgument()
        {
            DataModel test = new DataModel();
            await InternalComplexBooleanAsArgument(test);

        }

        private async Task InternalComplexBooleanAsArgument(DataModel test)
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => test.Boolean).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.ComplexBoolean), simpleConstant);
        }

        [Test]
        public async Task ThreeBooleans()
        {
            DataModel test = new DataModel();
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.Boolean && r.Boolean && r.Boolean).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.ThreeBooleans), simpleConstant);
        }

        [Test]
        public async Task ListInt()
        {
            List<int> integers = new List<int>() { 1, 2, 3 };
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => integers.Contains(r.Integer)).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.ListInt), simpleConstant);
        }


        [Test]
        public async Task ListIntFromProperty()
        {
            List<int> integers = new List<int>() { 1, 2, 3 };
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.ListInteger.Contains(1)).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.ListIntFromProperty), simpleConstant);
        }

        [Test]
        public async Task InnerLambda()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.ListInteger.Any(s => s == 1)).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.InnerLambda), simpleConstant);
        }

        [Test]
        public async Task NullableHasValue()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.OneToOneNullable.Integer.HasValue).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.NullableHasValue), simpleConstant);
        }

        [Test]
        public async Task NullableValue()
        {
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => r.OneToOneNullable.Integer.HasValue && r.OneToOneNullable.Integer.Value == 1).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.NullableValue), simpleConstant);
        }

        [Test]
        public async Task LinqlObject()
        {
            LinqlObject<DataModel> objectTest = new LinqlObject<DataModel>(new DataModel());
            Assert.That(objectTest.TypedValue, Is.EqualTo(objectTest.Value));
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => objectTest.TypedValue.Integer == r.Integer).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.LinqlObject), simpleConstant);
        }

        [Test]
        public async Task ObjectCalculationWithNull()
        {
            DataModel objectTest = new DataModel();
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => objectTest.OneToOne.Integer == r.Integer).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.ObjectCalculationWithNull), simpleConstant);
        }

        [Test]
        public async Task ObjectCalculationWithoutNull()
        {
            DataModel objectTest = new DataModel();
            objectTest.OneToOne = new DataModel();
            LinqlSearch<DataModel> search = Context.Set<DataModel>();
            string simpleConstant = await search.Where(r => objectTest.OneToOne.Integer == r.Integer).ToJsonAsync();
            TestLoader.Compare(nameof(Smoke_Test.ObjectCalculationWithoutNull), simpleConstant);
        }
    }

}