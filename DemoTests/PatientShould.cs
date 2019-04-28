using Demo;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTests
{
    public class PatientShould
    {
        [Fact]
        public void BeNewWhenCreated()
        {
            // Arrange
            var patient = new Patient();

            // Act
            var result = patient.IsNew;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HaveCorrectFullName()
        {
            var patient = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            var fullName = patient.FullName;
            Assert.Equal(expected: "Nick Carter", actual: fullName);
            Assert.StartsWith(expectedStartString: "Nick", actualString: fullName);
            Assert.EndsWith(expectedEndString: "Carter", actualString: fullName);
            Assert.Contains(expectedSubstring: "Nick Carter", actualString: fullName);
            Assert.Contains(expectedSubstring: "ck Car", actualString: fullName);

            Assert.NotEqual(expected: "NICK CARTER", actual: fullName);

            Assert.Matches(expectedRegexPattern: @"^[A-Z][a-z]*\s[A-Z][a-z]*", actualString: fullName);
        }

        [Fact]
        public void HaveDefaultBloodSugarWhenCreated()
        {
            var p = new Patient();
            var bloodSugar = p.BloodSugar;

            Assert.Equal(expected: 4.9f, actual: bloodSugar, precision: 5);  //precision 精度，小数点后几位
            Assert.InRange(actual: bloodSugar, low: 3.9f, high: 6.1f);  //区间
        }

        [Fact]
        public void HaveNoNameWhenCreated()
        {
            var p = new Patient();
            Assert.Null(p.FirstName);
            Assert.NotNull(p);
        }

        [Fact]
        public void HaveHadAColdBefore()
        {
            var p = new Patient();
            var diseases = new List<string>
            {
                "感冒",
                "发烧",
                "水痘",
                "腹泻"
            };

            p.History.Add(item: "感冒");
            p.History.Add(item: "发烧");
            p.History.Add(item: "水痘");
            p.History.Add(item: "腹泻");

            Assert.Contains(expected: "感冒", collection: p.History);
            Assert.DoesNotContain(expected: "心脏病", collection: p.History);

            // Predicate
            Assert.Contains(collection: p.History, filter: x => x.StartsWith("水"));
            Assert.All(p.History,
                action: x => Assert.True(x.Length >= 2));  //断定p.History里的元素长度>=2

            Assert.Equal(expected: diseases, actual: p.History);  //比较集合元素的值是否相等
        }

        [Fact]
        public void BeAPerson()
        {
            var p = new Patient();
            var p2 = new Patient();

            Assert.IsType<Patient>(p);    //断定是<T>类型
            Assert.IsNotType<Person>(p);  //断定不是<T>类型

            Assert.IsAssignableFrom<Person>(p); //断定派生自<T>类型

            Assert.NotSame(expected: p, actual: p2); //断定不是同一个实例
            //Assert.Same(expected: p, actual: p2);  //断定是同一个实例
        }

        [Fact]
        public void ThrowExceptionsWhenErrorOccurred()
        {
            var p = new Patient();

            var ex = Assert.Throws<InvalidOperationException>(testCode: () => p.NotAllowed());
            //Assert.Throws<InvalidOperationException>(testCode: () =>
            //{
            //    var patient = new Patient();
            //});

            Assert.Equal(expected: "not able to create", actual: ex.Message);
        }

        [Fact]
        public void RaiseSleptEvent()
        {
            var p = new Patient();

            Assert.Raises<EventArgs>(
                attach: handler => p.PatientSlept += handler, 
                detach: handler => p.PatientSlept -= handler, 
                testCode: () => p.Sleep());
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            var p = new Patient();

            Assert.PropertyChanged(p, propertyName: nameof(p.HeartBeatRate),
                testCode: () => p.IncreaseHeartBeatRate());
        }
    }
}
