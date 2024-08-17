# Community Support Platform

The **Community Support Platform** is a comprehensive web application designed to facilitate community engagement by allowing users to manage campaigns, articles, companies, donations, donors, and job postings. This project leverages ASP.NET MVC for the frontend, ASP.NET Web API for backend services, and Entity Framework for ORM (Object-Relational Mapping).

## Features

-   **Campaign Management**: Create, Read, Update, and Delete campaign data.
-   **Article Management**: Manage articles with full CRUD functionality.
-   **Company and Donation Management**: Track and manage donations and companies involved in community support.
-   **Donor Management**: Handle donor information and interactions.
-   **Job Management**: Post and manage job listings relevant to the community.
-   **Relational Data Handling**: Manage relationships between various entities like campaigns, articles, companies, donations, and donors.

## Running this Project

### Prerequisites

-   **Visual Studio 2019** or later
-   **.NET Framework 4.7.2**
-   **SQL Server** for database management

### Setup Steps

1.  **Clone the Repository**:
    ```bash    
    `git clone [repository link]` 
    ```
    
2.  **Open the Project**: Open the solution file `CommunitySupportPlatform.sln` in Visual Studio.
    
3.  **Set Target Framework**:
    
    -   Navigate to `Project > CommunitySupportPlatform Properties`.
    -   Change the target framework to `.NET Framework 4.7.2`.
4.  **Create Database**:
    
    -   Ensure there is an `App_Data` folder in the project (Right-click the solution > View in File Explorer, then create the folder if it doesn't exist).
    -   Open the Package Manager Console in Visual Studio.
    -   Run the command:
        ```bash
        `Update-Database` 
        ```
    -   Verify the database is created by navigating to `View > SQL Server Object Explorer > MSSQLLocalDb`.
5.  **Run the Application**: You can run the application using Visual Studio by hitting `F5` or selecting `Debug > Start Debugging`.
    

## Common Issues and Resolutions

-   **Could not attach .mdf database**: Ensure the `App_Data` folder is created in the project directory.
-   **Error: 'Type' cannot be null**: Update Entity Framework to the latest version.
-   **Exception has been thrown by the target of an invocation**: Ensure the project repository is cloned to the actual drive on the machine.
-   **System.Web.Http does not have reference to serialize**: Add a reference to `System.Web.Extensions`.

## API Endpoints

### Article Management

-   **Get List of Articles**:
    
    ```bash
    
    `curl https://localhost:44394/api/ArticleData/ListArticles` 
    ```
-   **Get Single Article**:
    
   ``` bash
    
    `curl https://localhost:44394/api/ArticleData/FindArticle/{id}` 
   
```
-   **Add a New Article**:
    
   ``` bash
        
    `curl -H "Content-Type:application/json" -d @article.json https://localhost:44394/api/ArticleData/AddArticle` 
   ```

-   **Delete an Article**:
    
   ``` bash
        
    `curl -d "" https://localhost:44394/api/ArticleData/DeleteArticle/{id}` 
   ```
-   **Update an Article**:
    
    ``` bash
        
    `curl -H "Content-Type:application/json" -d @article.json https://localhost:44394/api/ArticleData/UpdateArticle/{id}` 
    ```

### Company Management

-   **Get List of Companies**:
    
  ```  bash
      
    `curl https://localhost:44394/api/CompanyData/ListCompanies` 
```
-   **Get Single Company**:
    
  ```  bash
        
    `curl https://localhost:44394/api/CompanyData/FindCompany/{id}` 
   ``` 
-   **Add a New Company**:
    
   ``` bash
        
    `curl -H "Content-Type:application/json" -d @company.json https://localhost:44394/api/CompanyData/AddCompany` 
   ``` 
-   **Delete a Company**:
    
   ``` bash
        
    `curl -d "" https://localhost:44394/api/CompanyData/DeleteCompany/{id}` 
   ``` 
-   **Update a Company**:
    
  ```  bash
        
    `curl -H "Content-Type:application/json" -d @company.json https://localhost:44394/api/CompanyData/UpdateCompany/{id}` 
  ```  
-   **Get Companies Related to a Donation**:
    
   ``` bash
        
    `curl https://localhost:44394/api/CompanyData/ListCompaniesForDonation/{donationId}` 
    
```
### Donation Management

-   **Get List of Donations**:
    
    ```bash
    
    `curl https://localhost:44394/api/DonationData/ListDonations` 
    ```
-   **Get Single Donation**:
    
    ``` bash
      
    `curl https://localhost:44394/api/DonationData/FindDonation/{id}` 
    ```
-   **Add a New Donation**:
    
   ```  bash
        
    `curl -H "Content-Type:application/json" -d @donation.json https://localhost:44394/api/DonationData/AddDonation` 
  ```  
-   **Delete a Donation**:
    
  ```  bash
    
    
    `curl -d "" https://localhost:44394/api/DonationData/DeleteDonation/{id}` 
   ``` 
-   **Update a Donation**:
    
   ``` bash
      
    `curl -H "Content-Type:application/json" -d @donation.json https://localhost:44394/api/DonationData/UpdateDonation/{id}` 
    
```
### Donor Management

-   **Get List of Donors**:
    
 ```   bash
      
    `curl https://localhost:44394/api/DonorData/ListDonors` 
 ```   
-   **Get Single Donor**:
    
  ```  bash
      
    `curl https://localhost:44394/api/DonorData/FindDonor/{id}` 
  ```  
-   **Add a New Donor**:
    
 ```   bash
        
    `curl -H "Content-Type:application/json" -d @donor.json https://localhost:44394/api/DonorData/AddDonor` 
 ```   
-   **Delete a Donor**:
    
  ```  bash
        
    `curl -d "" https://localhost:44394/api/DonorData/DeleteDonor/{id}` 
   ``` 
-   **Update a Donor**:
    
   ``` bash
    
    
    `curl -H "Content-Type:application/json" -d @donor.json https://localhost:44394/api/DonorData/UpdateDonor/{id}` 
  ```  

### Job Management

-   **Get List of Jobs**:
    
  ```  bash
    
    
    `curl https://localhost:44394/api/JobData/ListJobs` 
  ```  
-   **Get Single Job**:
    
 ```   bash
    
    
    `curl https://localhost:44394/api/JobData/FindJob/{id}` 
  ```  
-   **Add a New Job**:
    
 ```   bash
    
    
    `curl -H "Content-Type:application/json" -d @job.json https://localhost:44394/api/JobData/AddJob` 
 ```   
-   **Delete a Job**:
    
 ```   bash
    
    
    `curl -d "" https://localhost:44394/api/JobData/DeleteJob/{id}` 
  ```  
-   **Update a Job**:
    
 ```   bash
    
    
    `curl -H "Content-Type:application/json" -d @job.json https://localhost:44394/api/JobData/UpdateJob/{id}` 
    
```
## Database Relationships

### Campaign, Article, Company, Donation, and Donor Relationships

-   **Campaign**: A campaign can have multiple articles, companies, donations, and donors associated with it.
-   **Article**: Articles are linked to campaigns and can be authored by specific users.
-   **Company**: Companies can participate in multiple campaigns and be involved in donations.
-   **Donation**: Donations are tied to specific campaigns and donors.
-   **Donor**: Donors can contribute to multiple campaigns and their contributions are tracked.

### Table Relationship

-   **Campaign Table**: `CampaignId` (Primary Key), `Title`, `Description`, `StartDate`, `EndDate`
-   **Article Table**: `ArticleId` (Primary Key), `Title`, `Content`, `CampaignId` (Foreign Key)
-   **Company Table**: `CompanyId` (Primary Key), `Name`, `Address`, `CampaignId` (Foreign Key)
-   **Donation Table**: `DonationId` (Primary Key), `Amount`, `DonorId` (Foreign Key), `CampaignId` (Foreign Key)
-   **Donor Table**: `DonorId` (Primary Key), `Name`, `Email`, `Phone`

Feel empowered to engage with your community through the Community Support Platform! Dive into the features and tools provided to make a positive impact on local initiatives. For additional guidance and troubleshooting, be sure to explore our comprehensive documentation and in-code comments.
