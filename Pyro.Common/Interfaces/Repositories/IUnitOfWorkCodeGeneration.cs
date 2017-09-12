﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This file was code generated by Pyro.CodeGeneration.Template.MainTemplate.tt
//Generation TimeStamp: 13/09/2017 8:24:41 AM

namespace Pyro.Common.Interfaces.Repositories
{
  public partial interface IUnitOfWork
  {
    IResourceRepository AccountRepository { get; }
    IResourceRepository ActivityDefinitionRepository { get; }
    IResourceRepository AdverseEventRepository { get; }
    IResourceRepository AllergyIntoleranceRepository { get; }
    IResourceRepository AppointmentRepository { get; }
    IResourceRepository AppointmentResponseRepository { get; }
    IResourceRepository AuditEventRepository { get; }
    IResourceRepository BasicRepository { get; }
    IResourceRepository BinaryRepository { get; }
    IResourceRepository BodySiteRepository { get; }
    IResourceRepository BundleRepository { get; }
    IResourceRepository CapabilityStatementRepository { get; }
    IResourceRepository CarePlanRepository { get; }
    IResourceRepository CareTeamRepository { get; }
    IResourceRepository ChargeItemRepository { get; }
    IResourceRepository ClaimRepository { get; }
    IResourceRepository ClaimResponseRepository { get; }
    IResourceRepository ClinicalImpressionRepository { get; }
    IResourceRepository CodeSystemRepository { get; }
    IResourceRepository CommunicationRepository { get; }
    IResourceRepository CommunicationRequestRepository { get; }
    IResourceRepository CompartmentDefinitionRepository { get; }
    IResourceRepository CompositionRepository { get; }
    IResourceRepository ConceptMapRepository { get; }
    IResourceRepository ConditionRepository { get; }
    IResourceRepository ConsentRepository { get; }
    IResourceRepository ContractRepository { get; }
    IResourceRepository CoverageRepository { get; }
    IResourceRepository DataElementRepository { get; }
    IResourceRepository DetectedIssueRepository { get; }
    IResourceRepository DeviceRepository { get; }
    IResourceRepository DeviceComponentRepository { get; }
    IResourceRepository DeviceMetricRepository { get; }
    IResourceRepository DeviceRequestRepository { get; }
    IResourceRepository DeviceUseStatementRepository { get; }
    IResourceRepository DiagnosticReportRepository { get; }
    IResourceRepository DocumentManifestRepository { get; }
    IResourceRepository DocumentReferenceRepository { get; }
    IResourceRepository EligibilityRequestRepository { get; }
    IResourceRepository EligibilityResponseRepository { get; }
    IResourceRepository EncounterRepository { get; }
    IResourceRepository EndpointRepository { get; }
    IResourceRepository EnrollmentRequestRepository { get; }
    IResourceRepository EnrollmentResponseRepository { get; }
    IResourceRepository EpisodeOfCareRepository { get; }
    IResourceRepository ExpansionProfileRepository { get; }
    IResourceRepository ExplanationOfBenefitRepository { get; }
    IResourceRepository FamilyMemberHistoryRepository { get; }
    IResourceRepository FlagRepository { get; }
    IResourceRepository GoalRepository { get; }
    IResourceRepository GraphDefinitionRepository { get; }
    IResourceRepository GroupRepository { get; }
    IResourceRepository GuidanceResponseRepository { get; }
    IResourceRepository HealthcareServiceRepository { get; }
    IResourceRepository ImagingManifestRepository { get; }
    IResourceRepository ImagingStudyRepository { get; }
    IResourceRepository ImmunizationRepository { get; }
    IResourceRepository ImmunizationRecommendationRepository { get; }
    IResourceRepository ImplementationGuideRepository { get; }
    IResourceRepository LibraryRepository { get; }
    IResourceRepository LinkageRepository { get; }
    IResourceRepository ListRepository { get; }
    IResourceRepository LocationRepository { get; }
    IResourceRepository MeasureRepository { get; }
    IResourceRepository MeasureReportRepository { get; }
    IResourceRepository MediaRepository { get; }
    IResourceRepository MedicationRepository { get; }
    IResourceRepository MedicationAdministrationRepository { get; }
    IResourceRepository MedicationDispenseRepository { get; }
    IResourceRepository MedicationRequestRepository { get; }
    IResourceRepository MedicationStatementRepository { get; }
    IResourceRepository MessageDefinitionRepository { get; }
    IResourceRepository MessageHeaderRepository { get; }
    IResourceRepository NamingSystemRepository { get; }
    IResourceRepository NutritionOrderRepository { get; }
    IResourceRepository ObservationRepository { get; }
    IResourceRepository OperationDefinitionRepository { get; }
    IResourceRepository OperationOutcomeRepository { get; }
    IResourceRepository OrganizationRepository { get; }
    IResourceRepository ParametersRepository { get; }
    IResourceRepository PatientRepository { get; }
    IResourceRepository PaymentNoticeRepository { get; }
    IResourceRepository PaymentReconciliationRepository { get; }
    IResourceRepository PersonRepository { get; }
    IResourceRepository PlanDefinitionRepository { get; }
    IResourceRepository PractitionerRepository { get; }
    IResourceRepository PractitionerRoleRepository { get; }
    IResourceRepository ProcedureRepository { get; }
    IResourceRepository ProcedureRequestRepository { get; }
    IResourceRepository ProcessRequestRepository { get; }
    IResourceRepository ProcessResponseRepository { get; }
    IResourceRepository ProvenanceRepository { get; }
    IResourceRepository QuestionnaireRepository { get; }
    IResourceRepository QuestionnaireResponseRepository { get; }
    IResourceRepository ReferralRequestRepository { get; }
    IResourceRepository RelatedPersonRepository { get; }
    IResourceRepository RequestGroupRepository { get; }
    IResourceRepository ResearchStudyRepository { get; }
    IResourceRepository ResearchSubjectRepository { get; }
    IResourceRepository RiskAssessmentRepository { get; }
    IResourceRepository ScheduleRepository { get; }
    IResourceRepository SearchParameterRepository { get; }
    IResourceRepository SequenceRepository { get; }
    IResourceRepository ServiceDefinitionRepository { get; }
    IResourceRepository SlotRepository { get; }
    IResourceRepository SpecimenRepository { get; }
    IResourceRepository StructureDefinitionRepository { get; }
    IResourceRepository StructureMapRepository { get; }
    IResourceRepository SubscriptionRepository { get; }
    IResourceRepository SubstanceRepository { get; }
    IResourceRepository SupplyDeliveryRepository { get; }
    IResourceRepository SupplyRequestRepository { get; }
    IResourceRepository TaskRepository { get; }
    IResourceRepository TestReportRepository { get; }
    IResourceRepository TestScriptRepository { get; }
    IResourceRepository ValueSetRepository { get; }
    IResourceRepository VisionPrescriptionRepository { get; }
  }
}
