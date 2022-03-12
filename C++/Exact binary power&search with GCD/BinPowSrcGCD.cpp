
//Note : Some of these solutions have been done based on tutorials.

#include<bits/stdc++.h>
#include<ext/pb_ds/assoc_container.hpp>
#include<ext/pb_ds/tree_policy.hpp>
#define ll long long 
const int nmax = 2e5,mdl = 1e9+7;
using namespace std;
using namespace __gnu_pbds;

template<class T> using oset = tree<T,null_type,less<T>,rb_tree_tag,tree_order_statistics_node_update>;
oset<int> jp;
// binpow binsearch gcd
int ar[nmax],n,m,k,p;

int binsrc(int val)
{
	sort(ar,ar+n);
	int l=0,r=n,index;
	while(l<r)
	{
		cout << "L:"<< l << "R:" << r << endl;
		index = (r+l) >> 1;
		if(val>ar[index])l=index+1;
		else r = index;
	}
	return r;
}

int gcd(int a,int b)
{
	while(b)
	{
		a %= b;
		int c = a;
		a = b;
		b = c; 
	}
	return a;
}

int binpow(int a,int b)
{
	int c = 1;
	while(b)
	{
		if(b&1)c*=a;
		a = a*a;
		b >>= 1;
	}
	return c;
}

int main()
{
	cin >> n >> p;
	//for(int i=0;i<n;i++)cin >> ar[i];
	//cout << binsrc(p) << endl;
	
	//cout << gcd(n,p) << endl;
	//cout << (n*p)/gcd(n,p) << endl;
	//cout << __gcd(n,p) << endl;
	
	//cout << binpow(n,p) << endl;
	//cout << (int)pow(n,p) << endl;
	
	for(int i=1;i<=n;i++)jp.insert(i);
	while(jp.size()-1)
	{
		k = (k+p)%jp.size();
		cout << *jp.find_by_order(k) << endl;
		jp.erase(*jp.find_by_order(k));
		k %= jp.size();
	}
	cout << *jp.find_by_order(k) << "\n";
}
