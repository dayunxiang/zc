                Release Notes for RSLinx OPC Custom Interface Program Samples
                -------------------------------------------------------------
Enclosed in this directory is a program sample, Connpt, demonstrating the
implementation of asynchronous read, asynchronous write and subscription using
COM Connection Points. The files included are:

	1. ConnPt.cpp - source file for the main program
	2. opc.h - opc header file
	3. opc_i.c - opc include file
	4. opccomn.h - opc common interface header file
	5. opccomn_i.c - opc common interface include file
	6. opcda.h - opc data access header file
	7. opcda_i.c - opc data access include file
	8. sink.cpp - source file containing 'sink' functions
	9. sink.h - header file for sink.cpp
	10. utils.cpp - source file containing utility functions
	11. utils.h - header file for utils.cpp
	12. connect.mak - makefile to build executable

The makefile 'connect.mak' is used with nmake to build an executable from these
files. Use the command line:

			nmake -f connect.mak

For information on connection points and how it is used with OPC, refer to the 
following resources:

	1. OPC Data Access Custom Interface Specification, 2.0
	2. Microsoft documentation for a description of the Connection
           Point concept (e.g.MSDN Library CDs).


