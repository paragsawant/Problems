Problem A
Design and implement an interface (IModelService) to perform operations with POCO objects (i.e. business models) of any type – only classes should be allowed.  
The interface should have the following 3 (three) public methods:
•	A method that will accept two POCO objects of any type and return a list of the differences. The POCO may contain nested objects; 
	some properties may be collections of POCOs (i.e. A Person model may contain a List<T> of Address models)
•	A difference is a model in itself with the name of the property (as a string) and the values of each property in each object (i.e. value 1 and value 2)
•	A method that will accept two POCO objects of any type and return a list of property names that are equal in the two objects.
•	A method that will compute the cryptographic hash of any POCO object.  
The implementation should have support for any of the cryptographic algorithms supported by the .NET framework. 
The hash returned by this method should be ready to be returned in a REST-styled web service (i.e. return the value as a string)
Expected Deliverables
1.	A working/running implementation that meets all requirements as stated.
2.	A sample console application that demonstrates the use of the interface implemented.
