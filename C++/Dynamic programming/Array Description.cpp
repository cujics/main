
//Task : https://cses.fi/problemset/task/1746/
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<bits/stdc++.h>
#define ll long long
#define VITA_BREVIS 0
const int nmax = 1e5 , mmax= 1e2+1 , mod = 1e9+7;
ll dp[nmax][mmax];
int ar[nmax];
int n,m;
using namespace std;
ll solve(ll i,ll k)
{
	ll ans = 0;
	if(i==n)return 1;
	if(dp[i][k]!=-1)return dp[i][k];
	if(k==0 && i==0)
	{
		for(int j=1;j<m+1;j++)
		{
			ans += solve(i+1,j);
		}
		dp[i][k]=ans % mod;
		return ans % mod;
	}
	if(ar[i]==0)
	{
		for(int j=k-1;j<=k+1;j++)
		{
			if(j<1 || j>m)continue;
			ans= (ans +solve(i+1,j))%mod;
		}
		dp[i][k]=ans%mod;
		return ans%mod;
	}
	if(abs(ar[i]-k)>1){
		dp[i][k]=0;
		return 0;	
	}
	ans = solve(i+1,ar[i]) % mod;
	dp[i][k]=ans % mod;
	return ans % mod;
}
int main()
{
	cin >> n >> m;
	memset(dp,-1,sizeof(dp));
	for(int i=0;i<n;i++)cin >> ar[i];
	cout << solve(0,ar[0]) << endl;
}
