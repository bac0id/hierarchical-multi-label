using HML;

namespace HML.Test {
	[TestClass]
	public class LabelTest {

		[TestMethod]
		public void TestInit() {
			string strApple = "Apple";
			Label labelApple = new Label(strApple);

			Assert.AreEqual(labelApple.Text, strApple);
			Assert.IsNull(labelApple.Description);
			Assert.IsNull(labelApple.Parent);
			Assert.IsNotNull(labelApple.Children);
			Assert.AreEqual(labelApple.Children.Count, 0);

			labelApple.Description = "An apple";
			Assert.AreEqual(labelApple.Description, "An apple");
		}

		[TestMethod]
		public void TestSetInvalidParentDuringInit() {
			Label labelApple;
			Assert.ThrowsException<ArgumentNullException>(() => labelApple = new Label("Apple", null));
		}

		[TestMethod]
		public void TestSetValidParentDuringInit() {
			Label labelFruit = new Label("Fruit");
			Label labelApple = new Label("Apple", labelFruit);

			Assert.AreEqual(labelFruit.Children.Count, 1);
			AssertParentRelationship(labelFruit, labelApple);
		}

		[TestMethod]
		public void TestAddInvalidChildAfterInit() {
			Label labelFruit = new Label("Fruit");
			Assert.ThrowsException<ArgumentNullException>(() => labelFruit.AddChild(null));
		}

		[TestMethod]
		public void TestAddValidChildAfterInit() {
			Label labelFruit = new Label("Fruit");
			Label labelApple = new Label("Apple");

			Assert.AreEqual(labelFruit.Children.Count, 0);

			labelFruit.AddChild(labelApple);
			Assert.AreEqual(labelFruit.Children.Count, 1);
			AssertParentRelationship(labelFruit, labelApple);
		}


		[TestMethod]
		public void TestSetInvalidParentWhenNoParent() {
			Label labelApple = new Label("Apple");
			Assert.ThrowsException<ArgumentNullException>(() => labelApple.SetParent(null));
		}

		[TestMethod]
		public void TestSetValidParentWhenNoParent() {
			Label labelFruit = new Label("Fruit");
			Label labelApple = new Label("Apple");

			Assert.AreEqual(labelFruit.Children.Count, 0);

			labelApple.SetParent(labelFruit);
			Assert.AreEqual(labelFruit.Children.Count, 1);
			AssertParentRelationship(labelFruit, labelApple);
		}

		[TestMethod]
		public void TestSetValidParentWhenHaveParent() {
			Label labelFood = new Label("Food");
			Label labelFruit = new Label("Fruit");
			Label labelApple = new Label("Apple");

			labelApple.SetParent(labelFruit);
			Assert.AreEqual(labelFruit.Children.Count, 1);
			AssertParentRelationship(labelFruit, labelApple);

			labelApple.SetParent(labelFood);
			Assert.AreEqual(labelFood.Children.Count, 1);
			AssertParentRelationship(labelFood, labelApple);

			Assert.AreEqual(labelFruit.Children.Count, 0);
			Assert.IsFalse(labelFruit.Children.Contains(labelApple));
		}


		private void AssertParentRelationship(Label parent, Label child) {
			Assert.AreEqual(child.Parent, parent);
			Assert.IsTrue(parent.Children.Contains(child));
		}
	}
}