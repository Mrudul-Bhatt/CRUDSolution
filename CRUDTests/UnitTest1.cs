namespace CRUDTests
{
    public class UnitTest1
    {
        //Fact means this method is for Unit Testing
        [Fact]
        public void Test1()
        {
            //Arrange - Declaration of variables and collecting inputs 
            //Act - Calling the method to be tested
            //Assert - Checking the output

            //Arrange
            Math math = new Math();

            int input1 = 10, input2 = 20;

            int expected = 30;

            //Act
            int result = math.Add(input1, input2);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}