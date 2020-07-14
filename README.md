# ClimbingShoebox
Description: E-commerce site using ASP.NET Core MVC, SQL Server and Bootstrap4
Credit: some core features (including basic presentation of products on cards and the shopping cart and order implementation) were designed based on Gill Cleeren's "Build web applications with ASP.NET Core MVC" course on Pluralsight.com

Entirely additional features added by me:
- Product search 
- Sort products by price and rating
- "Add to favourites" (i.e. users can favourite a product and see a page presenting their favourited products)
- Add comments to favourited products
- User rating of products
- Order history
- Remove item from cart 
- Store locations page with Google Map
- Organisation of products by brand

Note: 
- this uses Asp.net Identity Code First migration. You can compile the app and run: 'update-database' from the package manager console and then run the app.
- this uses SendGrid for email confirmation for account sign-up. To see all the features of the app you will need to setup your own SendGrid user and ApiKey, or simply remove this feature.
- this is NOT the final production version of the app - it is a work in progress!
