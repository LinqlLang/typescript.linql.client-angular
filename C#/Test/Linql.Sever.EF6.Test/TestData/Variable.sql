
SET IDENTITY_INSERT [test].[Variable] ON 

INSERT [test].[Variable] ([VariableID], [VariableName]) VALUES (3, N'Occupied Percent')
INSERT [test].[Variable] ([VariableID], [VariableName]) VALUES (1, N'Rent per Square Foot')
INSERT [test].[Variable] ([VariableID], [VariableName]) VALUES (2, N'Vacant Percent')
SET IDENTITY_INSERT [test].[Variable] OFF
