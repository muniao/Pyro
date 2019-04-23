﻿using System.Collections.Generic;
using Pyro.DataLayer.DbModel.EntityBase;
using Pyro.DataLayer.DbModel.EntityGenerated;

//This file was code generated by Pyro.CodeGeneration.Template.MainTemplate.tt
//Generation TimeStamp: 22/04/2019 10:57:31 AM
namespace Pyro.DataLayer.DbModel.Entity
{  
  public partial class _FhirRelease : ConfigEntityBase, IConfigEntityBase, IModelBase
  {   
    public _FhirRelease()
    {
      this.AccountList = new HashSet<AccountRes>();      
      this.ActivityDefinitionList = new HashSet<ActivityDefinitionRes>();      
      this.AdverseEventList = new HashSet<AdverseEventRes>();      
      this.AllergyIntoleranceList = new HashSet<AllergyIntoleranceRes>();      
      this.AppointmentList = new HashSet<AppointmentRes>();      
      this.AppointmentResponseList = new HashSet<AppointmentResponseRes>();      
      this.AuditEventList = new HashSet<AuditEventRes>();      
      this.BasicList = new HashSet<BasicRes>();      
      this.BinaryList = new HashSet<BinaryRes>();      
      this.BiologicallyDerivedProductList = new HashSet<BiologicallyDerivedProductRes>();      
      this.BodyStructureList = new HashSet<BodyStructureRes>();      
      this.BundleList = new HashSet<BundleRes>();      
      this.CapabilityStatementList = new HashSet<CapabilityStatementRes>();      
      this.CarePlanList = new HashSet<CarePlanRes>();      
      this.CareTeamList = new HashSet<CareTeamRes>();      
      this.CatalogEntryList = new HashSet<CatalogEntryRes>();      
      this.ChargeItemList = new HashSet<ChargeItemRes>();      
      this.ChargeItemDefinitionList = new HashSet<ChargeItemDefinitionRes>();      
      this.ClaimList = new HashSet<ClaimRes>();      
      this.ClaimResponseList = new HashSet<ClaimResponseRes>();      
      this.ClinicalImpressionList = new HashSet<ClinicalImpressionRes>();      
      this.CodeSystemList = new HashSet<CodeSystemRes>();      
      this.CommunicationList = new HashSet<CommunicationRes>();      
      this.CommunicationRequestList = new HashSet<CommunicationRequestRes>();      
      this.CompartmentDefinitionList = new HashSet<CompartmentDefinitionRes>();      
      this.CompositionList = new HashSet<CompositionRes>();      
      this.ConceptMapList = new HashSet<ConceptMapRes>();      
      this.ConditionList = new HashSet<ConditionRes>();      
      this.ConsentList = new HashSet<ConsentRes>();      
      this.ContractList = new HashSet<ContractRes>();      
      this.CoverageList = new HashSet<CoverageRes>();      
      this.CoverageEligibilityRequestList = new HashSet<CoverageEligibilityRequestRes>();      
      this.CoverageEligibilityResponseList = new HashSet<CoverageEligibilityResponseRes>();      
      this.DetectedIssueList = new HashSet<DetectedIssueRes>();      
      this.DeviceList = new HashSet<DeviceRes>();      
      this.DeviceDefinitionList = new HashSet<DeviceDefinitionRes>();      
      this.DeviceMetricList = new HashSet<DeviceMetricRes>();      
      this.DeviceRequestList = new HashSet<DeviceRequestRes>();      
      this.DeviceUseStatementList = new HashSet<DeviceUseStatementRes>();      
      this.DiagnosticReportList = new HashSet<DiagnosticReportRes>();      
      this.DocumentManifestList = new HashSet<DocumentManifestRes>();      
      this.DocumentReferenceList = new HashSet<DocumentReferenceRes>();      
      this.EffectEvidenceSynthesisList = new HashSet<EffectEvidenceSynthesisRes>();      
      this.EncounterList = new HashSet<EncounterRes>();      
      this.EndpointList = new HashSet<EndpointRes>();      
      this.EnrollmentRequestList = new HashSet<EnrollmentRequestRes>();      
      this.EnrollmentResponseList = new HashSet<EnrollmentResponseRes>();      
      this.EpisodeOfCareList = new HashSet<EpisodeOfCareRes>();      
      this.EventDefinitionList = new HashSet<EventDefinitionRes>();      
      this.EvidenceList = new HashSet<EvidenceRes>();      
      this.EvidenceVariableList = new HashSet<EvidenceVariableRes>();      
      this.ExampleScenarioList = new HashSet<ExampleScenarioRes>();      
      this.ExplanationOfBenefitList = new HashSet<ExplanationOfBenefitRes>();      
      this.FamilyMemberHistoryList = new HashSet<FamilyMemberHistoryRes>();      
      this.FlagList = new HashSet<FlagRes>();      
      this.GoalList = new HashSet<GoalRes>();      
      this.GraphDefinitionList = new HashSet<GraphDefinitionRes>();      
      this.GroupList = new HashSet<GroupRes>();      
      this.GuidanceResponseList = new HashSet<GuidanceResponseRes>();      
      this.HealthcareServiceList = new HashSet<HealthcareServiceRes>();      
      this.ImagingStudyList = new HashSet<ImagingStudyRes>();      
      this.ImmunizationList = new HashSet<ImmunizationRes>();      
      this.ImmunizationEvaluationList = new HashSet<ImmunizationEvaluationRes>();      
      this.ImmunizationRecommendationList = new HashSet<ImmunizationRecommendationRes>();      
      this.ImplementationGuideList = new HashSet<ImplementationGuideRes>();      
      this.InsurancePlanList = new HashSet<InsurancePlanRes>();      
      this.InvoiceList = new HashSet<InvoiceRes>();      
      this.LibraryList = new HashSet<LibraryRes>();      
      this.LinkageList = new HashSet<LinkageRes>();      
      this.ListList = new HashSet<ListRes>();      
      this.LocationList = new HashSet<LocationRes>();      
      this.MeasureList = new HashSet<MeasureRes>();      
      this.MeasureReportList = new HashSet<MeasureReportRes>();      
      this.MediaList = new HashSet<MediaRes>();      
      this.MedicationList = new HashSet<MedicationRes>();      
      this.MedicationAdministrationList = new HashSet<MedicationAdministrationRes>();      
      this.MedicationDispenseList = new HashSet<MedicationDispenseRes>();      
      this.MedicationKnowledgeList = new HashSet<MedicationKnowledgeRes>();      
      this.MedicationRequestList = new HashSet<MedicationRequestRes>();      
      this.MedicationStatementList = new HashSet<MedicationStatementRes>();      
      this.MedicinalProductList = new HashSet<MedicinalProductRes>();      
      this.MedicinalProductAuthorizationList = new HashSet<MedicinalProductAuthorizationRes>();      
      this.MedicinalProductContraindicationList = new HashSet<MedicinalProductContraindicationRes>();      
      this.MedicinalProductIndicationList = new HashSet<MedicinalProductIndicationRes>();      
      this.MedicinalProductIngredientList = new HashSet<MedicinalProductIngredientRes>();      
      this.MedicinalProductInteractionList = new HashSet<MedicinalProductInteractionRes>();      
      this.MedicinalProductManufacturedList = new HashSet<MedicinalProductManufacturedRes>();      
      this.MedicinalProductPackagedList = new HashSet<MedicinalProductPackagedRes>();      
      this.MedicinalProductPharmaceuticalList = new HashSet<MedicinalProductPharmaceuticalRes>();      
      this.MedicinalProductUndesirableEffectList = new HashSet<MedicinalProductUndesirableEffectRes>();      
      this.MessageDefinitionList = new HashSet<MessageDefinitionRes>();      
      this.MessageHeaderList = new HashSet<MessageHeaderRes>();      
      this.MolecularSequenceList = new HashSet<MolecularSequenceRes>();      
      this.NamingSystemList = new HashSet<NamingSystemRes>();      
      this.NutritionOrderList = new HashSet<NutritionOrderRes>();      
      this.ObservationList = new HashSet<ObservationRes>();      
      this.ObservationDefinitionList = new HashSet<ObservationDefinitionRes>();      
      this.OperationDefinitionList = new HashSet<OperationDefinitionRes>();      
      this.OperationOutcomeList = new HashSet<OperationOutcomeRes>();      
      this.OrganizationList = new HashSet<OrganizationRes>();      
      this.OrganizationAffiliationList = new HashSet<OrganizationAffiliationRes>();      
      this.ParametersList = new HashSet<ParametersRes>();      
      this.PatientList = new HashSet<PatientRes>();      
      this.PaymentNoticeList = new HashSet<PaymentNoticeRes>();      
      this.PaymentReconciliationList = new HashSet<PaymentReconciliationRes>();      
      this.PersonList = new HashSet<PersonRes>();      
      this.PlanDefinitionList = new HashSet<PlanDefinitionRes>();      
      this.PractitionerList = new HashSet<PractitionerRes>();      
      this.PractitionerRoleList = new HashSet<PractitionerRoleRes>();      
      this.ProcedureList = new HashSet<ProcedureRes>();      
      this.ProvenanceList = new HashSet<ProvenanceRes>();      
      this.QuestionnaireList = new HashSet<QuestionnaireRes>();      
      this.QuestionnaireResponseList = new HashSet<QuestionnaireResponseRes>();      
      this.RelatedPersonList = new HashSet<RelatedPersonRes>();      
      this.RequestGroupList = new HashSet<RequestGroupRes>();      
      this.ResearchDefinitionList = new HashSet<ResearchDefinitionRes>();      
      this.ResearchElementDefinitionList = new HashSet<ResearchElementDefinitionRes>();      
      this.ResearchStudyList = new HashSet<ResearchStudyRes>();      
      this.ResearchSubjectList = new HashSet<ResearchSubjectRes>();      
      this.RiskAssessmentList = new HashSet<RiskAssessmentRes>();      
      this.RiskEvidenceSynthesisList = new HashSet<RiskEvidenceSynthesisRes>();      
      this.ScheduleList = new HashSet<ScheduleRes>();      
      this.SearchParameterList = new HashSet<SearchParameterRes>();      
      this.ServiceRequestList = new HashSet<ServiceRequestRes>();      
      this.SlotList = new HashSet<SlotRes>();      
      this.SpecimenList = new HashSet<SpecimenRes>();      
      this.SpecimenDefinitionList = new HashSet<SpecimenDefinitionRes>();      
      this.StructureDefinitionList = new HashSet<StructureDefinitionRes>();      
      this.StructureMapList = new HashSet<StructureMapRes>();      
      this.SubscriptionList = new HashSet<SubscriptionRes>();      
      this.SubstanceList = new HashSet<SubstanceRes>();      
      this.SubstanceNucleicAcidList = new HashSet<SubstanceNucleicAcidRes>();      
      this.SubstancePolymerList = new HashSet<SubstancePolymerRes>();      
      this.SubstanceProteinList = new HashSet<SubstanceProteinRes>();      
      this.SubstanceReferenceInformationList = new HashSet<SubstanceReferenceInformationRes>();      
      this.SubstanceSourceMaterialList = new HashSet<SubstanceSourceMaterialRes>();      
      this.SubstanceSpecificationList = new HashSet<SubstanceSpecificationRes>();      
      this.SupplyDeliveryList = new HashSet<SupplyDeliveryRes>();      
      this.SupplyRequestList = new HashSet<SupplyRequestRes>();      
      this.TaskList = new HashSet<TaskRes>();      
      this.TerminologyCapabilitiesList = new HashSet<TerminologyCapabilitiesRes>();      
      this.TestReportList = new HashSet<TestReportRes>();      
      this.TestScriptList = new HashSet<TestScriptRes>();      
      this.ValueSetList = new HashSet<ValueSetRes>();      
      this.VerificationResultList = new HashSet<VerificationResultRes>();      
      this.VisionPrescriptionList = new HashSet<VisionPrescriptionRes>();      
      }
    
      public ICollection<AccountRes> AccountList { get; set; }        
      public ICollection<ActivityDefinitionRes> ActivityDefinitionList { get; set; }        
      public ICollection<AdverseEventRes> AdverseEventList { get; set; }        
      public ICollection<AllergyIntoleranceRes> AllergyIntoleranceList { get; set; }        
      public ICollection<AppointmentRes> AppointmentList { get; set; }        
      public ICollection<AppointmentResponseRes> AppointmentResponseList { get; set; }        
      public ICollection<AuditEventRes> AuditEventList { get; set; }        
      public ICollection<BasicRes> BasicList { get; set; }        
      public ICollection<BinaryRes> BinaryList { get; set; }        
      public ICollection<BiologicallyDerivedProductRes> BiologicallyDerivedProductList { get; set; }        
      public ICollection<BodyStructureRes> BodyStructureList { get; set; }        
      public ICollection<BundleRes> BundleList { get; set; }        
      public ICollection<CapabilityStatementRes> CapabilityStatementList { get; set; }        
      public ICollection<CarePlanRes> CarePlanList { get; set; }        
      public ICollection<CareTeamRes> CareTeamList { get; set; }        
      public ICollection<CatalogEntryRes> CatalogEntryList { get; set; }        
      public ICollection<ChargeItemRes> ChargeItemList { get; set; }        
      public ICollection<ChargeItemDefinitionRes> ChargeItemDefinitionList { get; set; }        
      public ICollection<ClaimRes> ClaimList { get; set; }        
      public ICollection<ClaimResponseRes> ClaimResponseList { get; set; }        
      public ICollection<ClinicalImpressionRes> ClinicalImpressionList { get; set; }        
      public ICollection<CodeSystemRes> CodeSystemList { get; set; }        
      public ICollection<CommunicationRes> CommunicationList { get; set; }        
      public ICollection<CommunicationRequestRes> CommunicationRequestList { get; set; }        
      public ICollection<CompartmentDefinitionRes> CompartmentDefinitionList { get; set; }        
      public ICollection<CompositionRes> CompositionList { get; set; }        
      public ICollection<ConceptMapRes> ConceptMapList { get; set; }        
      public ICollection<ConditionRes> ConditionList { get; set; }        
      public ICollection<ConsentRes> ConsentList { get; set; }        
      public ICollection<ContractRes> ContractList { get; set; }        
      public ICollection<CoverageRes> CoverageList { get; set; }        
      public ICollection<CoverageEligibilityRequestRes> CoverageEligibilityRequestList { get; set; }        
      public ICollection<CoverageEligibilityResponseRes> CoverageEligibilityResponseList { get; set; }        
      public ICollection<DetectedIssueRes> DetectedIssueList { get; set; }        
      public ICollection<DeviceRes> DeviceList { get; set; }        
      public ICollection<DeviceDefinitionRes> DeviceDefinitionList { get; set; }        
      public ICollection<DeviceMetricRes> DeviceMetricList { get; set; }        
      public ICollection<DeviceRequestRes> DeviceRequestList { get; set; }        
      public ICollection<DeviceUseStatementRes> DeviceUseStatementList { get; set; }        
      public ICollection<DiagnosticReportRes> DiagnosticReportList { get; set; }        
      public ICollection<DocumentManifestRes> DocumentManifestList { get; set; }        
      public ICollection<DocumentReferenceRes> DocumentReferenceList { get; set; }        
      public ICollection<EffectEvidenceSynthesisRes> EffectEvidenceSynthesisList { get; set; }        
      public ICollection<EncounterRes> EncounterList { get; set; }        
      public ICollection<EndpointRes> EndpointList { get; set; }        
      public ICollection<EnrollmentRequestRes> EnrollmentRequestList { get; set; }        
      public ICollection<EnrollmentResponseRes> EnrollmentResponseList { get; set; }        
      public ICollection<EpisodeOfCareRes> EpisodeOfCareList { get; set; }        
      public ICollection<EventDefinitionRes> EventDefinitionList { get; set; }        
      public ICollection<EvidenceRes> EvidenceList { get; set; }        
      public ICollection<EvidenceVariableRes> EvidenceVariableList { get; set; }        
      public ICollection<ExampleScenarioRes> ExampleScenarioList { get; set; }        
      public ICollection<ExplanationOfBenefitRes> ExplanationOfBenefitList { get; set; }        
      public ICollection<FamilyMemberHistoryRes> FamilyMemberHistoryList { get; set; }        
      public ICollection<FlagRes> FlagList { get; set; }        
      public ICollection<GoalRes> GoalList { get; set; }        
      public ICollection<GraphDefinitionRes> GraphDefinitionList { get; set; }        
      public ICollection<GroupRes> GroupList { get; set; }        
      public ICollection<GuidanceResponseRes> GuidanceResponseList { get; set; }        
      public ICollection<HealthcareServiceRes> HealthcareServiceList { get; set; }        
      public ICollection<ImagingStudyRes> ImagingStudyList { get; set; }        
      public ICollection<ImmunizationRes> ImmunizationList { get; set; }        
      public ICollection<ImmunizationEvaluationRes> ImmunizationEvaluationList { get; set; }        
      public ICollection<ImmunizationRecommendationRes> ImmunizationRecommendationList { get; set; }        
      public ICollection<ImplementationGuideRes> ImplementationGuideList { get; set; }        
      public ICollection<InsurancePlanRes> InsurancePlanList { get; set; }        
      public ICollection<InvoiceRes> InvoiceList { get; set; }        
      public ICollection<LibraryRes> LibraryList { get; set; }        
      public ICollection<LinkageRes> LinkageList { get; set; }        
      public ICollection<ListRes> ListList { get; set; }        
      public ICollection<LocationRes> LocationList { get; set; }        
      public ICollection<MeasureRes> MeasureList { get; set; }        
      public ICollection<MeasureReportRes> MeasureReportList { get; set; }        
      public ICollection<MediaRes> MediaList { get; set; }        
      public ICollection<MedicationRes> MedicationList { get; set; }        
      public ICollection<MedicationAdministrationRes> MedicationAdministrationList { get; set; }        
      public ICollection<MedicationDispenseRes> MedicationDispenseList { get; set; }        
      public ICollection<MedicationKnowledgeRes> MedicationKnowledgeList { get; set; }        
      public ICollection<MedicationRequestRes> MedicationRequestList { get; set; }        
      public ICollection<MedicationStatementRes> MedicationStatementList { get; set; }        
      public ICollection<MedicinalProductRes> MedicinalProductList { get; set; }        
      public ICollection<MedicinalProductAuthorizationRes> MedicinalProductAuthorizationList { get; set; }        
      public ICollection<MedicinalProductContraindicationRes> MedicinalProductContraindicationList { get; set; }        
      public ICollection<MedicinalProductIndicationRes> MedicinalProductIndicationList { get; set; }        
      public ICollection<MedicinalProductIngredientRes> MedicinalProductIngredientList { get; set; }        
      public ICollection<MedicinalProductInteractionRes> MedicinalProductInteractionList { get; set; }        
      public ICollection<MedicinalProductManufacturedRes> MedicinalProductManufacturedList { get; set; }        
      public ICollection<MedicinalProductPackagedRes> MedicinalProductPackagedList { get; set; }        
      public ICollection<MedicinalProductPharmaceuticalRes> MedicinalProductPharmaceuticalList { get; set; }        
      public ICollection<MedicinalProductUndesirableEffectRes> MedicinalProductUndesirableEffectList { get; set; }        
      public ICollection<MessageDefinitionRes> MessageDefinitionList { get; set; }        
      public ICollection<MessageHeaderRes> MessageHeaderList { get; set; }        
      public ICollection<MolecularSequenceRes> MolecularSequenceList { get; set; }        
      public ICollection<NamingSystemRes> NamingSystemList { get; set; }        
      public ICollection<NutritionOrderRes> NutritionOrderList { get; set; }        
      public ICollection<ObservationRes> ObservationList { get; set; }        
      public ICollection<ObservationDefinitionRes> ObservationDefinitionList { get; set; }        
      public ICollection<OperationDefinitionRes> OperationDefinitionList { get; set; }        
      public ICollection<OperationOutcomeRes> OperationOutcomeList { get; set; }        
      public ICollection<OrganizationRes> OrganizationList { get; set; }        
      public ICollection<OrganizationAffiliationRes> OrganizationAffiliationList { get; set; }        
      public ICollection<ParametersRes> ParametersList { get; set; }        
      public ICollection<PatientRes> PatientList { get; set; }        
      public ICollection<PaymentNoticeRes> PaymentNoticeList { get; set; }        
      public ICollection<PaymentReconciliationRes> PaymentReconciliationList { get; set; }        
      public ICollection<PersonRes> PersonList { get; set; }        
      public ICollection<PlanDefinitionRes> PlanDefinitionList { get; set; }        
      public ICollection<PractitionerRes> PractitionerList { get; set; }        
      public ICollection<PractitionerRoleRes> PractitionerRoleList { get; set; }        
      public ICollection<ProcedureRes> ProcedureList { get; set; }        
      public ICollection<ProvenanceRes> ProvenanceList { get; set; }        
      public ICollection<QuestionnaireRes> QuestionnaireList { get; set; }        
      public ICollection<QuestionnaireResponseRes> QuestionnaireResponseList { get; set; }        
      public ICollection<RelatedPersonRes> RelatedPersonList { get; set; }        
      public ICollection<RequestGroupRes> RequestGroupList { get; set; }        
      public ICollection<ResearchDefinitionRes> ResearchDefinitionList { get; set; }        
      public ICollection<ResearchElementDefinitionRes> ResearchElementDefinitionList { get; set; }        
      public ICollection<ResearchStudyRes> ResearchStudyList { get; set; }        
      public ICollection<ResearchSubjectRes> ResearchSubjectList { get; set; }        
      public ICollection<RiskAssessmentRes> RiskAssessmentList { get; set; }        
      public ICollection<RiskEvidenceSynthesisRes> RiskEvidenceSynthesisList { get; set; }        
      public ICollection<ScheduleRes> ScheduleList { get; set; }        
      public ICollection<SearchParameterRes> SearchParameterList { get; set; }        
      public ICollection<ServiceRequestRes> ServiceRequestList { get; set; }        
      public ICollection<SlotRes> SlotList { get; set; }        
      public ICollection<SpecimenRes> SpecimenList { get; set; }        
      public ICollection<SpecimenDefinitionRes> SpecimenDefinitionList { get; set; }        
      public ICollection<StructureDefinitionRes> StructureDefinitionList { get; set; }        
      public ICollection<StructureMapRes> StructureMapList { get; set; }        
      public ICollection<SubscriptionRes> SubscriptionList { get; set; }        
      public ICollection<SubstanceRes> SubstanceList { get; set; }        
      public ICollection<SubstanceNucleicAcidRes> SubstanceNucleicAcidList { get; set; }        
      public ICollection<SubstancePolymerRes> SubstancePolymerList { get; set; }        
      public ICollection<SubstanceProteinRes> SubstanceProteinList { get; set; }        
      public ICollection<SubstanceReferenceInformationRes> SubstanceReferenceInformationList { get; set; }        
      public ICollection<SubstanceSourceMaterialRes> SubstanceSourceMaterialList { get; set; }        
      public ICollection<SubstanceSpecificationRes> SubstanceSpecificationList { get; set; }        
      public ICollection<SupplyDeliveryRes> SupplyDeliveryList { get; set; }        
      public ICollection<SupplyRequestRes> SupplyRequestList { get; set; }        
      public ICollection<TaskRes> TaskList { get; set; }        
      public ICollection<TerminologyCapabilitiesRes> TerminologyCapabilitiesList { get; set; }        
      public ICollection<TestReportRes> TestReportList { get; set; }        
      public ICollection<TestScriptRes> TestScriptList { get; set; }        
      public ICollection<ValueSetRes> ValueSetList { get; set; }        
      public ICollection<VerificationResultRes> VerificationResultList { get; set; }        
      public ICollection<VisionPrescriptionRes> VisionPrescriptionList { get; set; }        
      
  }
}
