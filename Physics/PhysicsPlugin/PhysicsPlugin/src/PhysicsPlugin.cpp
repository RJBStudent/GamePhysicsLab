#include "PhysicsPlugin.h"

#include "Foo.h"

Foo* inst = 0;

int InitFoo(int f_new)
{
	if (!inst)
	{
		inst = new Foo(f_new);
		return 1;
	}
	return 0;
}

int DoFoo(int bar)
{
	if (inst)
	{
		int result = inst->doFoo(bar);
		return result;
	}
	return 0;
}

int TermFoo()
{
	if (inst)
	{
		delete inst;
		inst = 0;
		return 1;
	}
	return 0;
}