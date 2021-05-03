using PotatoUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	[SerializeField]
	private MinMax m_test;
	[SerializeField, Wrapped]
	private string m_testString;
}
