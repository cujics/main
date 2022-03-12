
//Task : https://cses.fi/problemset/task/1639
//Note : Some of these solutions have been done based on William Lin 150 problem set & other tutorials.

#include<iostream>
#include<cstring>
#define ll long long
ll dp[5005][5005];
char a[5005],b[5005];
using namespace std;
int main()
{
	cin >> a >> b;
	for(int i=0;i<=max(strlen(a),strlen(b));i++)dp[0][i]=i,dp[i][0]=i;
	
	for(int i=1;i<=strlen(a);i++)
	{
		for(int j=1;j<=strlen(b);j++)
		{
			ll c = dp[i-1][j-1];
			ll f = min(dp[i-1][j],dp[i][j-1]);
			dp[i][j]=min(c,f)+1;
			if(a[i-1]==b[j-1])dp[i][j]=dp[i-1][j-1];
		}
	}
	cout << dp[strlen(a)][strlen(b)];
}
