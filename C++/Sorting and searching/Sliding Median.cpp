
//Task : https://cses.fi/problemset/task/1076/
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<bits/stdc++.h>
#define ll long long
using namespace std;
const int nmax = 2e5;
ll n,x,ar[nmax],br[nmax],b=0,es;
set<array<ll,2>> u,d,r;
array<ll,2> res[nmax];
void pri()
{
	cout << (*--u.end())[0]<<" ";
}
int ret()
{
	return (*--u.end())[0];	
}
void prikazz()
{
	cout << "PRVA:"<<endl;
	for(auto ie = u.begin();ie!=u.end();ie++)cout << (*ie)[0]<< " ";cout << endl;
	for(auto ie = u.begin();ie!=u.end();ie++)cout << (*ie)[1] << " ";
	cout << endl << "DRUGA:" << endl;
	for(auto ie = d.begin();ie!=d.end();ie++)cout << (*ie)[0] << " ";cout << endl;
	for(auto ie = d.begin();ie!=d.end();ie++)cout << (*ie)[1] << " ";
	
}
int main()
{
	cin >> n >> x;
	for(int i=0;i<n;i++)cin >> ar[i];
	for(int i=0;i<x;i++)res[i][0]=ar[i],res[i][1]=i;
	int num = r.size();
	if(x%2)b=1;
	sort(res,res+x);
	for(int i=0;i<x;i++)if(i<x/2+b)u.insert({res[i][0],res[i][1]});else d.insert({res[i][0],res[i][1]});
	pri();
	for(int i=x,j=0;i<n;i++,j++){
		if(ar[j]<ret())
		{
			u.erase({ar[j],j});
			if(ar[i]<=ret())u.insert({ar[i],i});
			else {
				d.insert({ar[i],i});
				u.insert({(*d.begin())[0],(*d.begin())[1]});
				d.erase({(*d.begin())[0],(*d.begin())[1]});
		}
		}
		else if(ar[j]>ret())
		{
			d.erase({ar[j],j});
			if(ar[i]>=ret())d.insert({ar[i],i});
			else {
				u.insert({ar[i],i});
				d.insert({(*--u.end())[0],(*--u.end())[1]});
				u.erase({(*--u.end())[0],(*--u.end())[1]});
			}	
		}
		else
		{
			if(ar[i]>ret())u.erase({(*--u.end())[0],(*--u.end())[1]}),d.insert({ar[i],i}),u.insert({(*d.begin())[0],(*d.begin())[1]}),d.erase({(*d.begin())[0],(*d.begin())[1]});
			else u.erase({(*--u.end())[0],(*--u.end())[1]}),u.insert({ar[i],i});
		
		}
		pri();
	}
}
