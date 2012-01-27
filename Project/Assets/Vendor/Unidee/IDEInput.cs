/* See the copyright information at https://github.com/srfoster/Unidee/blob/master/COPYRIGHT */
using UnityEngine;
using System.Collections;

public abstract class IDEInput {

	public abstract string GetFileName();
	public abstract string GetCode();
	public abstract void SetCode(string code);
	public abstract void Process(IDE ide);
}
