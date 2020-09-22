
HOW TO HANDLE ENTITIES WITH INDEFINITE NUMBER OF PARAMETERS IN ASP. NET CORE MVC?

This repository includes a solution to the problem of creating insert/edit views for the entities that have indefinite number of parameters. Imagine that we have two entities called Lamp and Armature which have common fields, their own special fields but also an open-ended parameter list. So we need dynamically-created views reading parameter lists and puts the corresponding input fields. Parameters might be of any type (string, integer, date-time, select lists, ...) and may be mandatory, have a maximum length, etc.

To summarize the solution, BaseEntity Class is used for common tasks. (This class is Abstract because it has an abstract method called UpdateParameterValuesInDB).The most important task is to fetch the parameters and fill them with required select lists. This class is inherited by Lamp and Armature classes. The virtual method called GetParameters of BaseEntity class reads the common fields. Lamp and Armature classes calls this method first and then go on reading their own specific parameters.

Edit views of these entities read common fields from BaseEntity class and puts input fields for them (as well as their specific fields). These views then call a partial view called "_ParametersView" which iterates over the parameter lists of entities and create input fields depending on the parameter. After posting, BaseEntity class checks the values of input fields of parameters based on the definition of parameters. If it finds some problems (such as lack of data for a required parameter), it creates error states and add them to the model state by which the use can see what is wrong. JQuery-UI is used to produce user-friendly date-time fields.

In the real world, parameters should be read from a database of course. GetParameters method of BaseEntity class should be used for this purpose.

The project was developed in ASP NET Core MVC and covers the following topics:

    Using Partial Views
    Inheritance
    Abstract classes
    Virtual methods
    JQuery-UI

