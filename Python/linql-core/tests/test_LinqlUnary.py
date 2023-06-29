from src.linql_core.LinqlType import LinqlType
from src.linql_core.LinqlUnary import LinqlUnary
from src.linql_core.LinqlConstant import LinqlConstant
from .CustomTypeNameProvider import CustomTypeNameProvider

class TestLinqlUnary:

   typeNameProvider = CustomTypeNameProvider()

   def test_Constructor(self):
      unary = LinqlUnary("Test", [LinqlConstant(LinqlType.GetLinqlType(True, self.typeNameProvider), True)])
      assert unary.UnaryName == "Test"


   def test_Clone(self):
      unary = LinqlUnary("Test", [LinqlConstant(LinqlType.GetLinqlType(True, self.typeNameProvider), True)])
      clone = unary.Clone()
      assert unary.UnaryName == clone.UnaryName
