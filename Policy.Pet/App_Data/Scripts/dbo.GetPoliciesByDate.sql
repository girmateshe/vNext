CREATE PROCEDURE [dbo].[GetPoliciesByDate]
	@policyDay datetime
AS
	select count(*)
	from Policy
	where DateDiff(dd, PolicyDate, @policyDay) = 0
