-- 1. Enable UUID extension so Postgres can generate Guids for us
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS "ApplicationUsers" (
    "UserId" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Email" VARCHAR(50),      
    "Password" VARCHAR(100),   
    "FirstName" VARCHAR(50),  
    "LastName" VARCHAR(50),   
    "Gender" VARCHAR(20)       
    );