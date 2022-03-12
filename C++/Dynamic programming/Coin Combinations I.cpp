
//Task : https://cses.fi/problemset/task/1635
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<bits/stdc++.h>
using namespace std;
const int nmax=1e6;
const int modl=1e9+7;
int ct[nmax],read[nmax],n,k;
vector<int> coins;
int argb(int s,int g)
{
	if(g==2)return s;
	int ans=1e6;
	for(int i=0;i<g;i++)
	{
		int val =argb(s%ct[i],g-1);
		if(val<ans)ans=val;
	}
	cout << ans << endl;
	return ans;
}
int main()
{
	cin >> n >> k;
	for(int i=0;i<n;i++){
		cin >> ct[0];
		coins.push_back(ct[0]);
	}
	for(int i:coins)
	ct[0]=1;
	for(int i=1;i<k+1;i++)
	{
		for(int c:coins)
		{
			if(i-c>=0)ct[i]=(ct[i]+ct[i-c])%modl;
		}
	}
	if(ct[k]==1e6+1)ct[k]=-1;
	cout << ct[k];
}

