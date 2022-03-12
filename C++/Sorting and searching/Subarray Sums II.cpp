
//Task : https://cses.fi/problemset/task/1661
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<bits/stdc++.h>
#define ll long long
using namespace std;
const int nmax = 2e5;
int n,x,a[nmax],t;
ll sum=0,ans;
using namespace std;
int main()
{
	cin >> n >> x;
	map<ll,int> mp;
	mp[0]++;
	for(int i=0;i<n;i++)
	{
		cin >> a[i];
		sum+=a[i];
		ans+=mp[sum-x];
		mp[sum]++;
	}
	cout << ans;
	
}
