using NUnit.Framework;

namespace BrainFuckTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    // Echo until byte(255) encountred
    [Test]
    public void test1()
    {
        Assert.AreEqual("Codewars", new BrainFuck(",+[-.,+]","Codewars"+char.ConvertFromUtf32(255)).execute(), "Should return \"Codewars\" ");
    }

// Echo until byte(0) encountred
    [Test]
    public void test2()
    {
        Assert.AreEqual("Codewars", new BrainFuck(",[.[-],]", "Codewars" + char.ConvertFromUtf32(0)).execute(), "Should return \"Codewars\" ");
    }

// Two numbers multiplier
    [Test]
    public void test3()
    {
        Assert.AreEqual(char.ConvertFromUtf32(72), new BrainFuck(",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>.", char.ConvertFromUtf32(8) + char.ConvertFromUtf32(9)).execute(), "Should return H");
    }
}